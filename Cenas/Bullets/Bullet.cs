using Godot;
public partial class Bullet : Area2D
{
    [Export]
    public float speed = 0;
    [Export]
    public double damage = 0;
    [Export]
    public float knock;
    [Export]
    public float Durantion = 0;
    
    Vector2 direction = new Vector2();

    public override void _Ready()
    {
        GetNode<Timer>("Timer").Start(Durantion);
    }
    public override void _Process(double delta)
    {
        Position += Transform.X.Normalized() * -speed * (float)delta;
    }
    private void OnBody(Node2D body)
    {
        QueueFree();
        if (body is CharacterBody2D)
        {
            body.GetNode<HealthComponent>("HealthComponent").Hit(damage);
        }
        if (body is Enemy)
        {
            (body as Enemy).KnockBack(GlobalPosition,knock * 1000);
        }
    }
    private void OnTimeout()
    {
        QueueFree();
    }
}
