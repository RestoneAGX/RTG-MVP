using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Projectile : Stats
{
	public HitBox box;
	public float lifeTime;
	public float speed;
	public Rigidbody rb;
	
	internal override void Start()
	{
		base.Start();
		Destroy(gameObject, lifeTime);

		rb = GetComponent<Rigidbody>();
	}
	
	internal virtual void FixedUpdate()
	{
		if (stopped) return;
		
		// else if (box.parent != null)
		Atk();
		
		Move();
	}

	public virtual void Atk() { if (Hit.Projectile_Atk(box, transform)) Destroy(gameObject); }
	public virtual void Move() => rb.AddRelativeForce(Vector3.forward * speed, ForceMode.Impulse);

	public virtual void Spawn(Transform point, Transform parent) => Instantiate(this, point.position, point.rotation ).box.parent = parent;

    public void Spawn (Transform point, Transform parent, Action<Projectile> act) => act(Instantiate(this, point.position, point.rotation));

	internal virtual void DrawBoxes() => Hit.DrawHitBox(box);

	void OnDrawGizmosSelected() => DrawBoxes();
}
