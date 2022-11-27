using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sacred_SP : Standx
{
    public HitBox atk;
    public Disk base_disk;
    public float DashForce;
    public Transform point;

    public override void Neutral_Atk() => Hit.Atk(atk);
    public override void Forward_Atk() => Hit.Atk(atk);
    public override void Side_Atk() => base_disk.Launch(10f, 0f, point, parent); // Spawns with 10 force, and as a normal disk  
    public override void Back_Atk() => Hit.Atk(atk);

    public override void Neutral_SpAtk() => base_disk.Launch(10f, 1f, point, parent);
    public override void Forward_SpAtk() => Hit.Atk(atk); //HEAL
    public override void Side_SpAtk() => Hit.Effect(atk, 2, .5f);
    public override void Back_SpAtk() => Hit.Effect(atk, 2, 1f);

    public override void Aerial_Neutral_Atk() => Hit.Atk(atk);
	public override void Aerial_Forward_Atk() => parent.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * DashForce, ForceMode.Impulse);
	public override void Aerial_Side_Atk() => Hit.Atk(atk);
	public override void Aerial_Back_Atk() => Hit.Atk(atk);

	public override void Aerial_Neutral_SpAtk() {}
	public override void Aerial_Foward_SpAtk() => base_disk.Launch(10f, 2f, point, parent);
	public override void Aerial_Side_SpAtk() => Hit.Atk(atk);
	public override void Aerial_Back_SpAtk() => Hit.Atk(atk); // INHALE AKA inverse knockback
    public override void Ult() {}
    
    public override void DrawBoxes() => Hit.DrawHitBox(atk);

    public override void initialize() {
        base.initialize();
        atk.parent = parent;
    }
}
