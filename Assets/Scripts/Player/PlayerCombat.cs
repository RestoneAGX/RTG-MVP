public sealed class PlayerCombat : Combat{

	Movement movement;
	PlayerInput input;

	internal override void m_Start(){
		ani.SetBool("Standless", !standOn);
		movement = GetComponent<Movement>();
		input = InputManager.input;
		Inputs();
	}

	public void Atk() => Attack(movement.MvIn, "Atk");

	public void SpAtk() => Attack(movement.MvIn, "SpAtk");

    public void Block() => Block(!stats.blocking);

    public void Pose() => Pose(!ani.GetBool("Posing"));

	public void Summon(){
		if (stats.stopped) return;
		
		standOn = !standOn;
		
		stand.gameObject.SetActive(standOn);

		ani.SetBool("Standless", !standOn);
	}

    internal override void Inputs()
	{
		input.Combat.Atk.started += _ => Atk();
		input.Combat.SpAtk.started += _ => SpAtk();
		input.Combat.Summon.started += _ => Summon();
		input.Combat.Dash.started += _ => Block();
		input.Combat.Pose.started += _ => Pose();
	}
}