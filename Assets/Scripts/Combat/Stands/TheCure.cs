using UnityEngine;

public sealed class TheCure : Standx
{
    public Hitbox atk, wind_box;
    public GameObject wind_slash, reflector;
    public Transform firingPoint;

    public float dashForce, jumpForce, push_force;

    Movement movement;

    public override void Neutral_Atk() => atk.Atk(10 + stats.damageMultiplier);

    public override void Forward_Atk() => atk.Atk(0 + stats.damageMultiplier);

    public override void Side_Atk() => Instantiate(wind_slash, firingPoint.position, firingPoint.rotation);

    public override void Back_Atk() => atk.Atk(0 + stats.damageMultiplier);

    public override void Neutral_SpAtk(){
        wind_box.Atk(0 + stats.damageMultiplier);
        // TODO: Add force box pushing everything away
    }
    public override void Forward_SpAtk(){
        if (stats.storedDmg > 50){} // NOTE: 50 is abritrary
        // TODO: Switch into a different mode and add a multiplier
    }
    public override void Side_SpAtk() {
        if (stats.storedDmg > 10){ // TODO: Replace 10 with a minimum threashold
            movement.Force(dashForce);
            stats.storedDmg -= 10; // TODO: replace 10 with a loss amount
        }
    }
    public override void Back_SpAtk() => wind_box.Atk(-10);

    public override void Aerial_Neutral_Atk() => atk.Atk();
	public override void Aerial_Forward_Atk() {} // Bunny Jump; Don't know how that's going 
	public override void Aerial_Side_Atk() {} //Figure out how to do this
	public override void Aerial_Back_Atk() => atk.Atk();

	public override void Aerial_Neutral_SpAtk() {
        wind_box.Atk(0 + stats.damageMultiplier);
        Collider[] red = Physics.OverlapSphere(parent.position, wind_box.range, wind_box.opponent);

        foreach(Collider val in red){
            Rigidbody rb = val.GetComponent<Rigidbody>();
            if (rb != null)
                rb.AddExplosionForce(wind_box.damage, wind_box.point.position, wind_box.range, 1.5f, ForceMode.Impulse);
        }

        // TODO: Add force box pushing everything away
        movement.rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
	public override void Aerial_Foward_SpAtk() => movement.rb.AddForceAtPosition(parent.forward * push_force, firingPoint.position, ForceMode.Impulse); //NOTE: might want to change this up so, it's a box that launches anyone infront of it back
	public override void Aerial_Side_SpAtk() => reflector.SetActive(true); // NOTE: In the animation, deactivate the reflector's gameObject
	public override void Aerial_Back_SpAtk() => wind_box.Atk();

    public override void Ult() {}

    public override void initialize()
	{
        base.initialize();
        movement = GetComponentInParent<Movement>();
        stats.blockType = 1;
	}

    public override void DrawBoxes()
    {
        atk.DrawHitBox();
        wind_box.DrawHitBox();
    }
}
