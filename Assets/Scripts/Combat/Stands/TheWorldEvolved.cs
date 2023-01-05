using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class TheWorldEvolved : Standx
{
    public float timestop_duration, disk_force;
    // TODO: add vfx_obj_refs here 

    public void Fire(Projectile projectile) =>  projectile.Spawn(atk.point, parent);
    public void Disk_Launch(Disk projectile) => projectile.Launch(disk_force, -2, atk.point, parent);


    public void TimeStop() => Hit.Effect(atk, (int) Debuff.TimeStop, timestop_duration);

    public void Play_VFX(VisualEffect effect) => effect.Play();

    public void Play_Slash() {} // TODO: play the slash vfx
    public void Play_Beam() {} // TODO: Play beam vfx

    // TODO: add start and stop functions for: [Floating, Super armor]

    public override void Ult() {}

}
