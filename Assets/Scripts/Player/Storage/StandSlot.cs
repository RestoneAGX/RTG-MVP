using UnityEngine.UI;
using TMPro;

[System.Serializable]
public sealed class StandSlot
{
	public Standx stand;
	public Button button;
	public void Initialize()
	{
		if (stand == null) return;

		button.interactable = true;
		button.GetComponentInChildren<TMP_Text>().text = stand.name;
	}
	
	public void addStand(Standx newStand)
	{
		stand = newStand;
		button.interactable = true;
		button.GetComponentInChildren<TMP_Text>().text = stand.name;
	}
	
	public void removeStand()
	{
		stand = null;
		button.interactable = false;
		button.GetComponentInChildren<TMP_Text>().text = "[empty]";
	}
}
