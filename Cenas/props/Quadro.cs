using Godot;
using System;

public partial class Quadro : Sprite2D
{
    public override void _Ready()
    {
        Random RNG = new();
        if (RNG.Next(0,2) == 1)
        {
            RegionRect = new Rect2(RNG.Next(0,3) * 16,RNG.Next(0,4) * 32,16,32);
        }
        else 
        {
            QueueFree();
        }
    }
}
