using Godot;
using System;

public partial class SpawnManager : Node2D
{
    public void OnBody(Node2D Body)
    {
        foreach(Spawner i in GetChildren())
        {
            i.Spawn();
            GetParent().GetNode<Area2D>("Area").QueueFree();
            GD.Print("oi");
        }
    }
}

