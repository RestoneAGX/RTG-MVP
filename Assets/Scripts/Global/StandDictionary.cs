using UnityEngine;

public sealed class StandDictionary : MonoBehaviour
{
	public Standx[] entries;

	public static StandDictionary Dictionary;

	private void Awake() => Dictionary = this;
	
	public Standx FindBody(string targetStand)
	{
		for (int i = 0, z = entries.Length -1; i < entries.Length; i++, z--){
			if (entries[i].name == targetStand)
				return entries[i];
			if (entries[z].name == targetStand)
				return entries[z];
		}
		return null;
	}
}
