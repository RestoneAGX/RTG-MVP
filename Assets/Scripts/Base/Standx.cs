using UnityEngine;

[RequireComponent(typeof(Animator))]
public abstract class Standx : MonoBehaviour
{
	internal Transform parent;
	internal Stats stats;
	public Animator ani;
	
	#region Stand On
	public abstract void Neutral_Atk();
	public abstract void Forward_Atk();
	public abstract void Side_Atk();
	public abstract void Back_Atk();

	public abstract void Neutral_SpAtk();
	public abstract void Forward_SpAtk();
	public abstract void Side_SpAtk();
	public abstract void Back_SpAtk();

	public abstract void Aerial_Neutral_Atk();
	public abstract void Aerial_Forward_Atk();
	public abstract void Aerial_Side_Atk();
	public abstract void Aerial_Back_Atk();

	public abstract void Aerial_Neutral_SpAtk();
	public abstract void Aerial_Foward_SpAtk();
	public abstract void Aerial_Side_SpAtk();
	public abstract void Aerial_Back_SpAtk();

	public abstract void Ult();
	#endregion

	public virtual void initialize()
	{
		// NOTE: Set Player Animator Controller to Preset Stand Ani-Controller for simplicity when working with animations
		parent = transform.parent;
		stats = parent.GetComponent<Stats>();
		ani = GetComponent<Animator>();
	}

	public void Spawn(Transform point) => Instantiate(gameObject, point.position, point.rotation, point);

	public virtual void Despawn(){
		// NOTE: Reset Player Animator back to normal
		Destroy(gameObject);
	}

	public abstract void DrawBoxes();
	public virtual void ApplyAttributes(){}
	
	void Awake() => initialize();
	void Update() => ApplyAttributes();
	void OnDrawGizmosSelected() => DrawBoxes();
}
