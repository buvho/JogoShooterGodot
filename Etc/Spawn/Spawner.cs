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
    List<int> nL = new List<int>();
    public void Spawn()
    {
        ParticleSC = GD.Load<PackedScene>("res://Etc/Spawn/SpawnParticles.tscn");
        level = GetParent().GetParent<Node2D>();
        Tudo.kills -= quantidade;
        for (int i = 0; i < quantidade;i++)
        {
            int n = new Random().Next(1,GetChildCount());
            if (!nL.Contains(n)){
            CpuParticles2D Part = ParticleSC.Instantiate<CpuParticles2D>();
            level.AddChild(Part);
            Part.Position = GetChild<Node2D>(n).Position;
            nL.Add(n);
            partL.Add(Part);
            GetNode<Timer>("Timer").Start();
            } else {i--;}
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
