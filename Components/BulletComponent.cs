using Godot;
public partial class BulletComponent : Node2D
{
     [Export]
    public int speed;
    [Export]
    public double damage = 1;
    [Export]
    public float knock = 1000;
    public float Durantion;
    Area2D dad;
    
    Vector2 direction = new Vector2();

    public override void _Ready()
    {
        GetNode<Timer>("Timer").Start(Durantion);
    }
    public override void _Process(double delta)
    {
        Area2D dad = GetParent<Area2D>();
        dad.GlobalRotation = GlobalRotation;
        dad.Position += dad.Transform.X.Normalized() * -speed * (float)delta;
    }
    private void OnBody(Node2D body)
    {
        GetParent().QueueFree();
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
        GetParent().QueueFree();
    }
}
