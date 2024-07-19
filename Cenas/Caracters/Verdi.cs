using Godot;
using System;

public partial class Verdi : ShootingEnemy
{
    public override void Tick(double delta)
    {
        MoveToPlayer();
        TryShoot();
        Move(delta);
    }
}
