using UnityEngine;

public class Doll : Combat {
    public Vector3 MvIn;
	PlayerInput input;

	internal override void m_Start(){
		ani.SetBool("Standless", stand != null);
		input = InputManager.input;
		Inputs();
	}

	public void Atk() => Attack(MvIn, "Atk");

	public void SpAtk() => Attack(MvIn, "SpAtk");
    public void Pose() => Pose(!ani.GetBool("Posing"));

    internal override void Inputs()
	{
		input.Combat.Atk.started += _ => Atk();
		input.Combat.SpAtk.started += _ => SpAtk();
		input.Combat.Pose.started += _ => Pose();
	}
}
