using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : Stats
{
	public Hitbox box;
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
	
	public virtual void Atk(){ if(box.AtkProjectile(damageMultiplier, transform))	Destroy(gameObject); }
	
	public virtual void Move() => rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);

	internal virtual void DrawBoxes() => box.DrawHitBox();

	void OnDrawGizmosSelected() => DrawBoxes();
}
