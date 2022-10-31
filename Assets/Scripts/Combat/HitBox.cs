using System;
using UnityEngine;

[Serializable]
public sealed class Hitbox
{
    public Vector3 angle;
    public Color boxColor;
    public LayerMask opponent;
    public float range, damage;
    public Transform point;
    [HideInInspector] public Transform parent;

    public void Atk()
    {
        Collider[] plrs = Physics.OverlapSphere(point.position, range, opponent);
        foreach (Collider other in plrs)
        {
            if (other.transform == parent) continue;

            if (other.GetComponent<Rigidbody>())
               other.GetComponent<Rigidbody>().AddRelativeForce(angle, ForceMode.Impulse);

            other.GetComponent<Stats>().TakeDamage(damage);
        }
    }

    public void Atk(Action<Collider> act)
    {
        Collider[] plrs = Physics.OverlapSphere(point.position, range, opponent);
        foreach (Collider other in plrs)
        {
            if (other.transform == parent) continue;

            other.GetComponent<Stats>().TakeDamage(damage);
            act(other);
        }
    }

    public void Atk(float multiplier)
    {
        Collider[] plrs = Physics.OverlapSphere(point.position, range, opponent);
        foreach (Collider other in plrs)
        {
            if (other.transform == parent) continue;

            if (other.GetComponent<Rigidbody>())
                other.GetComponent<Rigidbody>().AddRelativeForce(angle, ForceMode.Impulse);

            other.GetComponent<Stats>().TakeDamage(damage + multiplier);
        }
    }

    public void AtkProjectile(float multiplier, Transform self)
    {
        Collider[] plrs = Physics.OverlapSphere(point.position, range, opponent);
        foreach (Collider other in plrs)
        {
            if (other.transform == parent || other.transform == self) continue;

            Debug.Log(other.name);
            other.GetComponent<Stats>().TakeDamage(multiplier + damage);
            other.GetComponent<Rigidbody>().AddRelativeForce(angle, ForceMode.Impulse);
            UnityEngine.Object.Destroy(self.gameObject);
        }

    }

    ///<Summary>
    /// Calls the attribtue system indirectly
    /// 0 is Time Stop
    /// 1 is Poison/bleed/freeze (Damage over time)
    /// 2 is Stun
    /// 3 is Tripping/Stun with animation
    /// 4 is visable/tracked
    ///</Summary>
    public void Effect(int AttributeType, float AttributeDuration)
    {
        Collider[] plrs = Physics.OverlapSphere(point.position, range, opponent);
        foreach (Collider other in plrs)
        {
            if (other.transform == parent) continue;
            other.GetComponent<StandAttribute>().StartDebuff(AttributeType, AttributeDuration);
        }
    }
    
    public void Effect(int attributeType, float attributeDuration, float attributeDamage)
    {
        Collider[] plrs = Physics.OverlapSphere(point.position, range, opponent);
        foreach (Collider other in plrs)
        {
            if (other.transform == parent) continue;
            other.GetComponent<StandAttribute>().StartDebuff(attributeType, attributeDuration, attributeDamage);
        }
    }

    public void DrawHitBox()
    {
        Gizmos.color = boxColor;
        Gizmos.DrawWireSphere(point.position, range);
    }
}
