using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : Projectile
{
    public override void Atk() {
        base.Atk();
        switch (speed){ // Speed acts as diskType because I don't want 2 GetComponent Calls
            case 1f: // Mini-TS
                Hit.Effect(box, 2, 1f);
                break;
        }
    }

    public override void Move() {}

    public void Launch(float launch_force, float variant, Transform point, Transform parent){
         Projectile x = Spawn(point, parent);
         x.speed = variant;
         x.rb.AddRelativeForce(Vector3.forward * launch_force, ForceMode.Impulse);
    }
}
