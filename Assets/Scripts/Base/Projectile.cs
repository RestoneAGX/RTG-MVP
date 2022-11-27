using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : Stats
{
	public HitBox box;
	public float lifeTime;
	public float speed;
	[HideInInspector] public Rigidbody rb;
	
	internal override void Start()
	{
		base.Start();
		Destroy(gameObject, lifeTime);

		rb = GetComponent<Rigidbody>();
	}
	
	internal virtual void FixedUpdate()
	{
		if (stopped) return;
		
		else if (box.parent != null) Atk();
		
		Move();
	}
	
	public virtual void Atk() { if (Hit.Projectile_Atk(box, transform)) Destroy(gameObject); }
	public virtual void Move() => rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);

	public virtual Projectile Spawn(Transform point, Transform parent) {
        Projectile a = Instantiate(gameObject, point.position, point.rotation ).GetComponent<Projectile>();
        a.box.parent = parent;
        return a;
    }

	internal virtual void DrawBoxes() => Hit.DrawHitBox(box);

	void OnDrawGizmosSelected() => DrawBoxes();
}
