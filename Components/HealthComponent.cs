using Godot;

public partial class HealthComponent : Node2D
{
    public double HitX = 1;
    [Signal]
    public delegate void HealthChangedEventHandler();
    [Signal]
    public delegate void DeadEventHandler();
    [Export]
    public double health;

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
        }

    }
    private void OnParticlesFinished()
    {
        GetParent().QueueFree();
    }
}
