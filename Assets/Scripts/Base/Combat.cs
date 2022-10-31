using UnityEngine;

[RequireComponent(typeof(Stats))]
public class Combat : MonoBehaviour
{
	[Header("Stand")] public Standx stand;
	public bool standOn;
	public int recovery;
	public Timer ultTimer; // TODO: Set a universal value for the max for this
	internal Animator ani;
	internal Stats stats;

	private void Start()
	{
		stats = GetComponent<Stats>();
		ani = GetComponentInChildren<Animator>(); // NOTE: A bug might appear where it call
		
		m_Start();
		standOn = (stand == null) ? false : true;
	}

	internal void Update()
	{
		ultTimer.UpdateTimer();
		m_Update();
	}

	#region Basics

	public void PoseHeal() => stats.hp += recovery;

	public void Pose(bool newState)
	{
		if (stats.stopped) return;

		ani.SetBool("Posing", newState);
		if (standOn) stand.ani.SetBool("Posing", newState);
	}

	public void Block(bool blocking)
	{
		if (stats.stopped) return;

		stats.blocking = blocking;
		ani.SetBool("Blocking", blocking);
		if (standOn) stand.ani.SetBool("Blocking", blocking);
	}

	#endregion

	#region Normal Stand on and off attacks

	public void Attack(Vector3 mvIn, string output){
		if (stats.stopped) return;

		if (mvIn.z > 0) // TODO: Change Zero to activation threashold
			output = "Forward_" + output;
		else if (mvIn.z < 0)
			output = "Back_" + output;
		else if (mvIn.x != 0)
			output = "Side_" + output;
		else
			output = "Neutral_" + output;

		// ani.SetTrigger(output);
		if (standOn) stand.ani.SetTrigger(output);
	}

	//Figure out how to make Ultimate cutscene thing
	public void Ult()
	{
		if(ultTimer.isRunning || stats.stopped) return;
		
		if (standOn) stand.Ult();
		else ani.SetTrigger("Ult"); //Fill in until we add a proper ultimate technique
	}

	#endregion

	internal virtual void Inputs(){}

	internal virtual void m_Start(){}

	internal virtual void m_Update(){}
}