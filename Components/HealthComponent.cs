using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using Godot;
using Godot.Collections;

public partial class HealthComponent : Node2D
{
    
    [Signal]
    public delegate void HealthChangedEventHandler();
    [Signal]
    public delegate void DeadEventHandler();
    [Export]
    public double health;
    public double HitX = 1;

    public override void _Ready()
    {
    }

    public void Heal(double heal)
        {
        health += heal;
        EmitSignal(SignalName.HealthChanged);
        }
        public void Hit(double damage)
        { 
        health -= damage * HitX;
        EmitSignal(SignalName.HealthChanged);
        if (health <= 0)
        {

            GetParent().GetNode<AnimatedSprite2D>("Sprite").Visible = false;
            GetParent().GetNode<CollisionShape2D>("Collision").QueueFree();
            GetNode<CpuParticles2D>("Particles").Emitting = true;
            EmitSignal(SignalName.Dead);
            Tudo.kills++;
        }
        }
    private void OnParticlesFinished()
    {
        GetParent().QueueFree();
    }
}
