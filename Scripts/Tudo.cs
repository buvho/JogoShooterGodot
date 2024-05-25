using System.Collections.Generic;
using System.Linq;
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

    public override void _Ready()
    {
        ProcessMode = ProcessModeEnum.Always;
    }
    public void NoUI()
    {
        GetTree().Root.GetNode("World/Player").GetNode<CanvasLayer>("VidaUI").Visible = false;
        GetTree().Root.GetNode("World/Player").GetNode<CanvasLayer>("ArmaUI").Visible = false;
    }
    public void BackUI()
    {
        GetTree().Root.GetNode("World/Player").GetNode<CanvasLayer>("VidaUI").Visible = true;
        GetTree().Root.GetNode("World/Player").GetNode<CanvasLayer>("ArmaUI").Visible = true;
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
}
