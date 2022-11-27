using System;
using UnityEngine;

[Serializable]
public struct HitBox {
    public float range, damage;
    public Vector3 angle;
    public Transform point;
    public Color color;
    public LayerMask opponent;
    [HideInInspector] public Transform parent;

    public HitBox(float range, float damage, Transform point){
        this.range = range;
        this.damage = damage;
        this.point = point;
        this.parent = point;
        this.color = Color.red;
        this.angle = Vector3.zero;
        this.opponent = LayerMask.GetMask("Player");
        Hit.Atk(this);
    }
}

public static class Hit {
    public static void Atk(HitBox box) {
        foreach ( Collider other in Physics.OverlapSphere(box.point.position, box.range, box.opponent) )
        {
            if (other.transform == box.parent) continue;
            other.GetComponent<Rigidbody>().AddRelativeForce(box.angle, ForceMode.Impulse);
            other.GetComponent<Stats>().TakeDamage(box.damage);
        }
    }

    public static void Atk(HitBox box, Action<Collider> act) {
        foreach (Collider other in Physics.OverlapSphere(box.point.position, box.range, box.opponent))
        {
            if (other.transform == box.parent) continue;
            other.GetComponent<Stats>().TakeDamage(box.damage);
            act(other);
        }
    }

    public static bool Projectile_Atk(HitBox box, Transform transform) {
        foreach ( Collider other in Physics.OverlapSphere(box.point.position, box.range, box.opponent) )
        {
            if (other.transform == box.parent || other.transform == transform) continue;
            other.GetComponent<Rigidbody>().AddRelativeForce(box.angle, ForceMode.Impulse);
            other.GetComponent<Stats>().TakeDamage(box.damage);
            return true;
        }
        return false;
    }


    public static void Effect(HitBox box, int AttributeType, float AttributeDuration)
    {
        foreach ( Collider other in Physics.OverlapSphere(box.point.position, box.range, box.opponent) )
            if (other.transform != box.parent)
                other.GetComponent<StandAttribute>().StartDebuff(AttributeType, AttributeDuration);
    }

    public static void Effect(HitBox box, int attributeType, float attributeDuration, float attributeDamage)
    {
        foreach ( Collider other in Physics.OverlapSphere(box.point.position, box.range, box.opponent) )
            if (other.transform != box.parent)
                other.GetComponent<StandAttribute>().StartDebuff(attributeType, attributeDuration, attributeDamage);
    }

    public static void DrawHitBox(HitBox box)
    {
        Gizmos.color = box.color;
        Gizmos.DrawWireSphere(box.point.position, box.range);
    }
}
