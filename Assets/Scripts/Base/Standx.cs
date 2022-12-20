using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Standx : MonoBehaviour
{
	public HitBox atk;
	public Animator ani;
	internal Stats stats;
	internal Transform parent;

	public void Atk() {
        Hit.SetAsSphere(atk, out atk.found_opps);
        Hit.Atk(atk);
    }

    public void BoxAtk() {
        Hit.SetAsBox(atk, out atk.found_opps);
        Hit.Atk(atk);
    }
	
	public abstract void Ult();

	public void Pause_Player() => stats.stopped = true;
	public void Resume_Player() => stats.stopped = false;

	public void Spawn(Transform point) => Instantiate(this, point.position, point.rotation, point).initialize();

	public virtual void Despawn(){
		// NOTE: Reset Player Animator back to normal
		Destroy(gameObject);
	}

	public virtual void initialize()
	{
		// NOTE: Set Player Animator Controller to Preset Stand Ani-Controller for simplicity when working with animations
		parent = transform.parent;
		stats = parent.GetComponent<Stats>();
		atk.buffer = new Collider[25];
		atk.parent = parent;
	}

	public virtual void DrawBoxes() => Hit.DrawHitBox(atk);

	public virtual void ApplyAttributes(){}
	
	void Awake() => initialize();
	void Update() => ApplyAttributes();
	void OnDrawGizmosSelected() => DrawBoxes();
}
