using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using Godot;


public partial class Tudo : Node
{
    public static List<string> loot = new()
    {
        "res://Cenas/Equipamentos/boot.tscn",
        "res://Cenas/Equipamentos/AmuletoFaca.tscn",
        "res://Cenas/Equipamentos/CoraçãoInfernal.tscn",
        "res://Cenas/Guns/Uzi.tscn",
        "res://Cenas/Guns/sniper.tscn",
        "res://Cenas/Guns/Shotgun.tscn",
        "res://Cenas/Equipamentos/BalaChumbo.tscn"
    };

    public int PauseState = 0;
    public static int kills = 0;

    public static PackedScene itemSC;
    public static Player P;

    public override void _Ready()
    {
        P = GetTree().Root.GetNode<Player>("World/Player");
        itemSC = GD.Load<PackedScene>("res://Etc/Item.tscn");
        ProcessMode = ProcessModeEnum.Always;
    }
    public void NoUI()
    {
        GetTree().Root.GetNode<CanvasLayer>("World/Player/VidaUI").Visible = false;
        GetTree().Root.GetNode<CanvasLayer>("World/Player/ArmaUI").Visible = false;
    }
    public void BackUI()
    {
        GetTree().Root.GetNode<CanvasLayer>("World/Player/VidaUI").Visible = true;
        GetTree().Root.GetNode<CanvasLayer>("World/Player/ArmaUI").Visible = true;
    }

    public override void _Input(InputEvent @event)
    {
       if (@event is InputEventKey KeyEvent && KeyEvent.Pressed && !KeyEvent.Echo && KeyEvent.Keycode == Key.Escape)
       {
        if(PauseState == 0)
        {
            PauseState = 1;
        }else
        {
            PauseState = 3;
        }
       }

    }
    public static PackedScene GetLoot()
    {

        int R;
        while (true){
        R = new Random().Next(0,loot.Count);
        PackedScene itemS = GD.Load<PackedScene>(loot[R]);
        bool ra = false;
        Node2D iten = (Node2D)itemSC.Instantiate();
        if (iten is Gun)
        {
           ra = P.guns.Contains(itemS);
        }
        else if (iten is Gear)
        {
            foreach(Gear i in P.GetNode("equipados").GetChildren().Cast<Gear>())
            {
                if(i.Nome == (iten as Gear).Nome) {ra = true;}
            }
            foreach(Gear i in P.GetNode("inventario").GetChildren().Cast<Gear>())
            {
                if(i.Nome == (iten as Gear).Nome) {ra = true;}
            }
        }
        if (!ra)
        {
        return itemS;
        }
        }
    }
}
