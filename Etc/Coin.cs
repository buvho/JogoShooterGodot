using Godot;
using System;
using System.ComponentModel.DataAnnotations.Schema;

public partial class Coin : Area2D
{
    public double vel = 40;
    Vector2 Rot;
    public void OnBody(Node2D body)
    {
        if (body is Player p)
        {
            p.CoinChange(1);
            QueueFree();
        }
    }
    public override void _Ready()
    {
    Random RNG = new Random();
    Rot = new Vector2(RNG.Next(-30,31),RNG.Next(-30,31)).Normalized();
    }
    public override void _Process(double delta)
    {
        if (vel > 0.1){
        Position += Rot * (float)(vel * delta);
        vel -= 50 * delta;
        }
    }
}
