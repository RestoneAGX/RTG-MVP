using System;
using UnityEngine;

[Serializable]
public struct HitBox {
    public float square, range, damage;
    public Vector3 angle;
    public Transform point;
    public Color color;
    public LayerMask opps;
    [HideInInspector] public Transform parent;
    [HideInInspector] public Collider[] buffer;
    [HideInInspector] public int found_opps;

    public HitBox(float range, float damage, Transform point){
        this.square = 0;
        this.range = range;
        this.damage = damage;
        this.point = point;
        this.parent = point;
        this.color = Color.red;
        this.angle = Vector3.zero;
        this.opps = LayerMask.GetMask("Player");
        this.buffer = new Collider[25];
        this.found_opps = 0;
        Hit.Atk(this);
    }
}

public static class Hit {
    public static void SetAsSphere(HitBox box, out int found) => found = Physics.OverlapSphereNonAlloc(box.point.position, box.range, box.buffer, box.opps);

    public static void SetAsBox(HitBox box, out int found) => found = Physics.OverlapBoxNonAlloc(box.point.position, new Vector3(box.square, box.square, box.range), box.buffer, Quaternion.identity,  box.opps);

    public static void DrawHitBox(HitBox box) {
        Gizmos.color = box.color;
        if (box.square != 0)
            Gizmos.DrawWireCube(box.point.position, new Vector3 (box.square, box.square, box.range));
        else
            Gizmos.DrawWireSphere(box.point.position, box.range);
    }

    public static void Atk(HitBox box) {
        for (int i = 0; i < box.found_opps; i++) 
        {
            if (box.buffer[i].transform == box.parent) continue;
            box.buffer[i].GetComponent<Rigidbody>().AddForce(box.point.TransformDirection(box.angle), ForceMode.Impulse);
            box.buffer[i].GetComponent<Stats>().TakeDamage(box.damage);
        }
    }

    public static void Atk(HitBox box, Action<Collider> act) {
        for (int i = 0; i < box.found_opps; i++) 
        {
            if (box.buffer[i].transform == box.parent) continue;
            box.buffer[i].GetComponent<Stats>().TakeDamage(box.damage);
            act(box.buffer[i]);
        }
    }

    public static bool Projectile_Atk(HitBox box, Transform transform) {
        
        for (int i = 0; i < box.found_opps; i++) 
        {
            if (box.buffer[i].transform == box.parent || box.buffer[i].transform == transform) continue;
            box.buffer[i].GetComponent<Rigidbody>().AddForce(box.angle, ForceMode.Impulse);
            box.buffer[i].GetComponent<Stats>().TakeDamage(box.damage);
            return true;
        }
        return false;
    }


    public static void Effect(HitBox box, int AttributeType, float AttributeDuration)
    {
        for (int i = 0; i < box.found_opps; i++) 
            if (box.buffer[i].transform != box.parent)
                box.buffer[i].GetComponent<StandAttribute>().StartDebuff(AttributeType, AttributeDuration);
    }

    public static void Effect(HitBox box, int attributeType, float attributeDuration, float attributeDamage)
    {
        for (int i = 0; i < box.found_opps; i++) 
            if (box.buffer[i].transform != box.parent)
                box.buffer[i].GetComponent<StandAttribute>().StartDebuff(attributeType, attributeDuration, attributeDamage);
    }
}
