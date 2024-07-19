using Godot;
using System;

public partial class VerdiElite : ShootingEnemy
{
bool d = false;
    public override void Tack()
    {
        TryShoot(0);
        GetNode<Timer>("Timer").Start(0.5);
    }
    public override void Tick(double delta)
    {
        MoveToPlayer();
        Move(delta);
    }
    public void OnTimeOut()
    {
        if (d)
        {
        TryShoot(0);
        d = false;
        } else 
        {
        TryShoot(1);
        d = true;
        }
    }
}


