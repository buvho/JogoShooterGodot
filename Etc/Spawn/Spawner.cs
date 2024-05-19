using Godot;
using System;
using System.Collections.Generic;

public partial class Spawner : Node2D
{
    PackedScene ParticleSC;
    [Export]
    PackedScene enemySC;
    [Export]
    int quantidade;
    Node2D level;
    List<CpuParticles2D> partL = new List<CpuParticles2D>();
    public void Spawn()
    {
        ParticleSC = GD.Load<PackedScene>("res://Etc/Spawn/SpawnParticles.tscn");
        level = GetParent().GetParent<Node2D>();
        for (int i = 0; i < quantidade;i++)
        {
            CpuParticles2D Part = ParticleSC.Instantiate<CpuParticles2D>();
            level.AddChild(Part);
            int n = GetChildCount()-1;
            Part.GlobalPosition = GetChild<Node2D>(new Random().Next(1,n)).GlobalPosition;
            GetChild(n).QueueFree();
            partL.Add(Part);
            GetNode<Timer>("Timer").Start();
        }
    }
    public void OnTimeOut()
    {
        foreach(CpuParticles2D i in partL){
        Enemy emy = enemySC.Instantiate<Enemy>();
        level.AddChild(emy);
        emy.GlobalPosition = i.GlobalPosition;
        i.QueueFree();
        }
        QueueFree();
    }
}
