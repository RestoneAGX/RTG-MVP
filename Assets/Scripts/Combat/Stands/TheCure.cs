using UnityEngine;

public sealed class TheCure : Standx
{
    public HitBox wind_box;
    public Transform firingPoint;
    public Projectile wind_slash;
    public float dashForce, jumpForce, push_force;
    Movement movement;

    public void Windbar_Cost(float cost) => stats.storedDmg -= cost;

    // public void Atk() => Hit.Atk(atk); // USE NEGATIVE TO HEAL, USE POSITIVE FOR ATTACK In ANI

    // public override void Aerial_Neutral_Atk() => Hit.Atk(atk); // NOTE: Maybe add wind particles


    public void Side_Atk() => wind_slash.Spawn(firingPoint, parent);

    public void Neutral_SpAtk() => Hit.Atk(wind_box,
        (Collider hit) => hit.GetComponent<Rigidbody>().AddExplosionForce(wind_box.damage,wind_box.point.position, wind_box.range, 1.5f, ForceMode.Impulse));

    public void Forward_SpAtk(){
        if (stats.storedDmg > 50){} // TODO: Switch into a different mode and add a multiplier
    }
    public void Side_SpAtk() {
        if (stats.storedDmg > 10){ // TODO: Replace 10 with a minimum threashold
            movement.Force(dashForce);
            stats.storedDmg -= 10; // TODO: replace 10 with a loss amount
        }
    }

	public void Aerial_Forward_Atk() {} // Bunny Jump; Don't know how that's going
	public void Aerial_Side_Atk() {} //Figure out how to do this

    // NOTE: Call Neutral_SpAtk() to do the damage and call this function to add the jump
	public void Aerial_Neutral_SpAtk() => movement.rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
	public void Aerial_Foward_SpAtk() => movement.rb.AddForceAtPosition(parent.forward * push_force, firingPoint.position, ForceMode.Impulse); //NOTE: might want to change this up so, it's a box that launches anyone infront of it back
	public void Aerial_Side_SpAtk() => Hit.Atk(wind_box, (Collider hit) =>
        {
            Projectile projectile = hit.GetComponent<Projectile>();
            projectile.box.parent = parent;
            projectile.rb.velocity = new Vector3( projectile.rb.velocity.x, projectile.rb.velocity.y, -projectile.rb.velocity.z);
            hit.transform.Rotate(Vector3.up * 180, Space.Self);
        });

    public override void Ult() {}

    public override void initialize()
	{
        base.initialize();
        movement = parent.GetComponent<Movement>();
        wind_box.parent = parent;
        stats.blockType = 1;
	}

    public override void DrawBoxes()
    {
        base.DrawBoxes();
        Hit.DrawHitBox(wind_box);
    }
}
