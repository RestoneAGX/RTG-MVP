using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerCombat : Combat{

	Movement movement;
	PlayerInput input;

	public int[] atk_hashes = new int[17];
	public Dictionary<int, bool> atk_dict;

	internal override void m_Start(){
		ani.SetBool("Standless", stand != null);
		movement = GetComponent<Movement>();
		input = InputManager.input;
		// setup_hashes(); // TODO: turn this on
		Inputs();
	}

	public void Atk() => Attack(movement.MvIn, "Atk");

	public void SpAtk() => Attack(movement.MvIn, "SpAtk");

	public void Block() {
		if (stats.blocking){
			Collider[] plrs = Physics.OverlapSphere(transform.position, 1f, movement.plr);
			bool val = false;
			int i = 0;
			for (; i < plrs.Length || val; i++)
				atk_dict.TryGetValue(plrs[i].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).shortNameHash, out val);

			if (val) plrs[i].GetComponent<StandAttribute>().StartDebuff(2, 1.5f);
		}
		
		Block(!stats.blocking);
	}

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

	private void setup_hashes(){
		atk_dict.Add(Animator.StringToHash("Neutral Atk"), true);
		atk_dict.Add(Animator.StringToHash("Forward Atk"), true);
		atk_dict.Add(Animator.StringToHash("Side Atk"), true);
		atk_dict.Add(Animator.StringToHash("Back Atk"), true);

		atk_dict.Add(Animator.StringToHash("Neutral SpAtk"), true);
		atk_dict.Add(Animator.StringToHash("Forward SpAtk"), true);
		atk_dict.Add(Animator.StringToHash("Side SpAtk"), true);
		atk_dict.Add(Animator.StringToHash("Back SpAtk"), true);

		atk_dict.Add(Animator.StringToHash("Aerial Neutral Atk"), true);
		atk_dict.Add(Animator.StringToHash("Aerial Forward Atk"), true);
		atk_dict.Add(Animator.StringToHash("Aerial Side Atk"), true);
		atk_dict.Add(Animator.StringToHash("Aerial Back Atk"), true);

		atk_dict.Add(Animator.StringToHash("Aerial Neutral SpAtk"), true);
		atk_dict.Add(Animator.StringToHash("Aerial Forward SpAtk"), true);
		atk_dict.Add(Animator.StringToHash("Aerial Side SpAtk"), true);
		atk_dict.Add(Animator.StringToHash("Aerial Back SpAtk"), true);
		// atk_dict.Add(UnityEngine.Animator.StringToHash("Parry-Grab"), true); // TODO: Parry-Grab String_Hash
	}
}