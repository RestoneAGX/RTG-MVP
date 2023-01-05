using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : Projectile
{
    public int diskType;

    public override void Atk() {
        if (Hit.Projectile_Atk(box, transform)){
            if (diskType > -1) Hit.Effect(box, diskType, 1f);
            else if (diskType == -2)
                Hit.Atk(box, (Collider hit) => hit.GetComponent<Rigidbody>().AddExplosionForce(box.damage * 10, box.point.position, box.range, 3f, ForceMode.Impulse));
            Destroy(gameObject);
        }

    }

    public override void Move() {}

    public void Launch(float launch_force, int variant, Transform point, Transform parent){
         Spawn(point, parent, (Projectile a) => {
             Disk x = (Disk) a;
             x.diskType = variant;
             x.rb.AddRelativeForce(Vector3.forward * launch_force, ForceMode.Impulse);
        });
    }
}
