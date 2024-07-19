using Godot;
using System;



public partial class ShootingEnemy : Enemy
{
    public void TryShoot(int index = 0)
    {
        if ((playe.GlobalPosition - GlobalPosition).Length() <= AtackRange * 10)
        {
            if (raycast.GetCollider() == playe)
            {
                sprite.GetChild<Gun>(index).Target(playe.GlobalPosition);
                sprite.GetChild<Gun>(index).Shoot();
            }
        }
    } 
}