using UnityEngine;

public sealed class TheCure : Standx
{
    public Hitbox atk, wind_box;
    public Transform firingPoint;
    public Projectile wind_slash;
    public float dashForce, jumpForce, push_force;
    Movement movement;

    public override void Neutral_Atk() => atk.Atk(stats.damageMultiplier);

    public override void Forward_Atk() => atk.Atk(stats.damageMultiplier);

    public override void Side_Atk() => wind_slash.Spawn(firingPoint, parent);

    public override void Back_Atk() => atk.Atk(stats.damageMultiplier);

    public override void Neutral_SpAtk() => wind_box.Atk((Collider hit) => hit.GetComponent<Rigidbody>().AddExplosionForce(wind_box.damage, wind_box.point.position, wind_box.range, 1.5f, ForceMode.Impulse));

    public override void Forward_SpAtk(){
        if (stats.storedDmg > 50){} // TODO: Switch into a different mode and add a multiplier
    }
    public override void Side_SpAtk() {
        if (stats.storedDmg > 10){ // TODO: Replace 10 with a minimum threashold
            movement.Force(dashForce);
            stats.storedDmg -= 10; // TODO: replace 10 with a loss amount
        }
    }
    public override void Back_SpAtk() => wind_box.Atk(-10);

    public override void Aerial_Neutral_Atk() => atk.Atk(stats.damageMultiplier);
	public override void Aerial_Forward_Atk() {} // Bunny Jump; Don't know how that's going 
	public override void Aerial_Side_Atk() {} //Figure out how to do this
	public override void Aerial_Back_Atk() => atk.Atk(stats.damageMultiplier);

    // NOTE: Call Neutral_SpAtk() to do the damage and call this function to add the jump
	public override void Aerial_Neutral_SpAtk() => movement.rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
	public override void Aerial_Foward_SpAtk() => movement.rb.AddForceAtPosition(parent.forward * push_force, firingPoint.position, ForceMode.Impulse); //NOTE: might want to change this up so, it's a box that launches anyone infront of it back
	public override void Aerial_Side_SpAtk() => wind_box.Atk((Collider hit) =>
        {
            Projectile projectile = hit.GetComponent<Projectile>();
            projectile.box.parent = parent;
            projectile.rb.velocity = new Vector3( projectile.rb.velocity.x, projectile.rb.velocity.y, -projectile.rb.velocity.z);
            hit.transform.Rotate(Vector3.up * 180, Space.Self);
        });
	public override void Aerial_Back_SpAtk() => wind_box.Atk(stats.damageMultiplier);

    public override void Ult() {}

    public override void initialize()
	{
        base.initialize();
        movement = parent.GetComponent<Movement>();
        wind_box.parent = parent;
        atk.parent = parent;
        stats.blockType = 1;
	}

    public override void DrawBoxes()
    {
        atk.DrawHitBox();
        wind_box.DrawHitBox();
    }
}
