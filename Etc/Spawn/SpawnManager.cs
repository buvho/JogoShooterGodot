using Godot;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public partial class SpawnManager : Node2D
{
    bool aq = false;

    public override void _PhysicsProcess(double delta)
    {
        if (aq && Tudo.kills == 0)
        {
            GetParent().GetNode<Node2D>("Portas").QueueFree();
            QueueFree();
        }
    }
    public void OnBody(Node2D Body)
    {
        if (Body is Player){
            Tudo.kills = 0;
            foreach(Spawner i in GetChildren().Cast<Spawner>())
            {
                i.Spawn();
                GetParent().GetNode<Area2D>("Area").QueueFree();
                GetParent().GetNode<Node2D>("Portas").ProcessMode = ProcessModeEnum.Inherit;
                GetParent().GetNode<Node2D>("Portas").Visible = true;
            }
            aq = true;
        }
    }
}

