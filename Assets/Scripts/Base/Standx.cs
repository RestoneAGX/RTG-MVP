using UnityEngine;

public abstract class Standx : MonoBehaviour
{
	internal Transform parent;
	internal Stats stats;
	
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

	public virtual void ApplyAttributes(){}
	
	#region Maintenence
	
	public virtual void initialize()
	{
		// NOTE: Set Player Animator Controller to Preset Stand Ani-Controller for simplicity when working with animations
		stats = GetComponentInParent<Stats>();
		parent = transform.parent;
	}

	public virtual void despawn(){
		// NOTE: Reset Player Animator back to normal
		Destroy(gameObject);
	}

	public abstract void DrawBoxes();
	#endregion
	
	void Awake() => initialize();	
	
	void OnDrawGizmosSelected() => DrawBoxes();
}
