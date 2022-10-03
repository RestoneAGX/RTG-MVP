using UnityEngine;

public sealed class Standless : MonoBehaviour
{
	public Hitbox atk;

	public void Atk() => atk.Atk();
	
}
