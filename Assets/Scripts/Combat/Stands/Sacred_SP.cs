using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacred_SP : Standx
{
    public Disk base_disk;
    public float dash_force, disk_force;

    public void Disk_Launch(int disk_type) => base_disk.Launch(disk_force, disk_type, atk.point, parent);

    // public void Side_Atk() => base_disk.Launch(10f, 0, point, parent); // Spawns with 10 force, and as a normal disk

    public void Side_SpAtk() => Hit.Effect(atk, (int) Debuff.Stun, 2.5f);
    public void Back_SpAtk() => Hit.Effect(atk, (int) Debuff.Stun, 5f);

	public void Aerial_Forward_Atk() => parent.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * dash_force, ForceMode.Impulse);

	public void Aerial_Neutral_SpAtk() {}
	// public override void Aerial_Back_SpAtk() => Hit.Atk(atk); // INHALE AKA inverse knockback
    public override void Ult() {}
}
