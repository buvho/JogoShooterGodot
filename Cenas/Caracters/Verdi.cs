using Godot;
using System;

public partial class Verdi : Enemy
{
    bool alive = true;
    public override void _Process(double delta)
    {
        if (alive)
        {
            SearchPlayer();
            Erotation();
            MoveToPlayer();
            TryShoot();
            Move(delta);
        }
    }
    public void Dead()
    {
        alive = false;
    }
}
