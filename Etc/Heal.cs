using Godot;
using System;

public partial class Heal : Area2D
{
    public double vel = 40;
    Vector2 Rot;
    public void OnBody(Node2D body)
    {
        if (body is Player p && p.maxhealth > p.GetNode<HealthComponent>("HealthComponent").health)
        {
            p.GetNode<HealthComponent>("HealthComponent").Heal(1);
            QueueFree();
        }
         if (body is TileMap)
        {
            vel = 0;
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
