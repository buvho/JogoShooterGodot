using Godot;
using System;

public partial class SpawnManager : Node2D
{
    public void OnBody()
    {
        foreach(Spawner i in GetChildren())
        {
            i.Spawn();
        }
    }
}

