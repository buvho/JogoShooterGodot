using Godot;
using System;
using System.Collections.Generic;

public partial class Spawner : Node2D
{
    [Export]
    PackedScene enemySC;

    [Export]
    int quantidade;
    public void Spawn()
    {
        Node2D level = GetParent().GetParent<Node2D>();
        for (int i = 0; i > quantidade;i++)
        {
            Enemy emy = enemySC.Instantiate<Enemy>();
            level.AddChild(emy);
            emy.GlobalPosition = GetChild<Node2D>(new Random().Next(0,GetChildCount() -1)).GlobalPosition;
        }
    }
}
