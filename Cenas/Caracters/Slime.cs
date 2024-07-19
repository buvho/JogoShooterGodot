using Godot;
using System;

public partial class Slime : Enemy
{
    public override void Tack()
    {
        Tudo.kills -= 3;
    }
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
            playe.Dash = (playe.GlobalPosition - GlobalPosition).Normalized() * 100000;
        }
    }

    public override void Ded()
    {
        PackedScene slimesSC = (PackedScene)GD.Load("res://Cenas/Caracters/sliminho.tscn");
        for (int i = 0; i < 3; i++){
        Enemy slimes = (Enemy)slimesSC.Instantiate();
        CallDeferred(Node2D.MethodName.AddSibling, slimes);
        slimes.SetDeferred("position",Position);
        }
    }
}

