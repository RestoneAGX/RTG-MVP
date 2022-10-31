using UnityEngine;

public sealed class StandStorage : MonoBehaviour
{
    public StandSlot[] slots = new StandSlot[6];
    public PlayerCombat combat;

    private void Awake(){ for (int i = 0; i < slots.Length; i++) slots[i].Initialize();}

    public void SwitchStand(int idx)
    {     
        if (combat.stand != null) combat.stand.Despawn();
        
        combat.ani.SetBool("Standless", false);
        slots[idx].stand.Spawn(combat.transform);
        combat.standOn = true;
    }

    public void AddToStorage(Standx newStand)
    {
        for (int i = 0; i <= slots.Length; i++)
        {
            if (!slots[i].stand)
            {
                slots[i].addStand(newStand);
                break;
            }
        }
    }

    public void RemoveStand(int idx) => slots[idx].removeStand();

    public void AddToExternalStorage(StandStorage external, int idx) => external.AddToStorage(slots[idx].stand);
}
