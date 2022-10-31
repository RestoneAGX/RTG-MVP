using UnityEngine;
using Cinemachine;

public sealed class Movement : MonoBehaviour
{
    public float sensetivity, speed, jumpForce, dashForce;
    public short jumps = 2, maxJumps = 2;
    public Timer dashCooldown;
    public Vector3 MvIn;

    [Header("Camera")] public Transform cam, focus_target;
    // public CinemachineTargetGroup targetGroup;
    // public float focus_range;
    public bool focusing;
    float smoothVelocity;

    [HideInInspector] public Animator ani;
    [HideInInspector] public PlayerInput input;
    [HideInInspector] public LayerMask ground, plr;
    [HideInInspector] public Rigidbody rb;
    // Player stats;

    private void Start()
    {
        rb = GetComponentInChildren<Rigidbody>();
        ani = GetComponentInChildren<Animator>();
        // stats = GetComponent<Player>();

        ground = LayerMask.GetMask("Ground");
        plr = LayerMask.GetMask("Player");
        input = InputManager.input;

        Cursor.lockState = CursorLockMode.Locked;
        inputs();
    }

    private void FixedUpdate()
    {
        dashCooldown.UpdateTimer();

        // if (stats.stopped || !ani.GetCurrentAnimatorStateInfo(0).IsName("Idle")) return;

        float x = input.Movement.Horizontal.ReadValue<float>();
        float y = input.Movement.Vertical.ReadValue<float>();

        // ani.SetFloat("x", x);
        // ani.SetFloat("y", y);

        MvIn = new Vector3(x, 0f, y);

        if (focusing) transform.LookAt(focus_target, Vector3.up);
        else if (MvIn.magnitude >= .1f)
        {
            float targetAngle = Mathf.Atan2(x, y) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref smoothVelocity, sensetivity);

            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            rb.MovePosition(transform.position + ((Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward).normalized * speed / 10));
        }
            rb.MovePosition(transform.position + MvIn * speed/10);
    }

    public void Jump()
    {
        // if (stats.stopped) return; // NOTE: Remove this if we have an animation that calls this function | Also, breaks control flow

        if (Physics.CheckSphere(transform.position + Vector3.down, .25f, ground))
            jumps = maxJumps;

        if (jumps > 0)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            jumps -= 1;
        }
    }

    public void Dash()
    {
        // if (dashCooldown.isRunning || stats.stopped) return; // NOTE: Remove stats.stopped if animation is for this

        dashCooldown.Start();
        Force(dashForce);        
    }

    public void Force(float dashForce) => rb.AddForce(transform.TransformDirection(MvIn) * dashForce, ForceMode.Impulse);

    private void inputs()
    {
        input.Combat.Dash.started += _ => Dash();
        input.Movement.Jump.started += _ => Jump();
        // input.Camera.Focus.started += _ => Focus();
        input.Other.Focus.started += _ => focusing = !focusing;

        // input.Movement.Jump.started += _ => ani.SetTrigger("Jumping", true);
        // input.Movement.Dash.started += _ => ani.SetTrigger("Dashing", true);
    }
}
