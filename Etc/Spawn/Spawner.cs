using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Spawner : Node2D
{
    [Export]
    PackedScene enemySC;
    [Export]
    int quantidade;

    [ExportGroup("iteminfo")]
    [Export]
    bool weak;
    [Export]
    bool medium;
    [Export]
    bool strong;
    [Export]
    bool solo;
    PackedScene ParticleSC;
    Node2D level;
    List<CpuParticles2D> partL = new List<CpuParticles2D>();
    List<int> nL = new List<int>();

    List<string> weakL = new()
     {
        "res://Cenas/Caracters/Verdi.tscn",
     };
     List<string> mediumL = new()
     {
        "res://Cenas/Caracters/Roxu.tscn",
        "res://Cenas/Caracters/sprinkler.tscn",
        "res://Cenas/Caracters/Slime.tscn",
     };
     List<string> strongL = new()
     {
        "res://Cenas/Caracters/Tatu.tscn",
        "res://Cenas/Caracters/VerdiElite.tscn"
     }; 
    List<string> selected = new();
    public void Spawn()
    {
    if(!solo)
    {
        if (weak){selected = selected.Concat(weakL).ToList();}
        if (medium){selected = selected.Concat(mediumL).ToList();}
        if (strong){selected = selected.Concat(strongL).ToList();}
        enemySC = GD.Load<PackedScene>(selected[new Random().Next(0,selected.Count)]);
    }

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
