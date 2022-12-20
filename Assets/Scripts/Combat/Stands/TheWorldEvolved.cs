using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheWorldEvolved : Standx
{
    public float timestop_duration;
    // TODO: add vfx_obj_refs here 

    public void Fire(Projectile projectile) =>  projectile.Spawn(atk.point, parent);

    public void TimeStop() => Hit.Effect(atk, (int) Debuff.TimeStop, timestop_duration);

    public void Play_Slash() {} // TODO: play the slash vfx
    public void Play_Beam() {} // TODO: Play beam vfx

    // TODO: add start and stop functions for: [Floating, Super armor]

    public override void Ult() {}

}
