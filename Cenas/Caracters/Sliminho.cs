using Godot;
using System;

public partial class Sliminho : Enemy
{
    public override void Tick(double delta)
    {
        MoveToPlayer();
        Move(delta);
    }

    public void OnBody(Node2D Body)
    {
        if (Body is Player)
        {
            playe.GetNode<HealthComponent>("HealthComponent").Hit(1);
            playe.Dash = (playe.GlobalPosition - GlobalPosition).Normalized() * 18000;
        }
    }

    
}