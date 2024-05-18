using Godot;

public partial class HitBox : Area2D
{
    [Export]
    public float knock;
    [Export]
    public double damage;
    public int tocou;

private void OnBody(Node2D body)
    {
        tocou = 1;
        if (body is CharacterBody2D)
        {
            body.GetNode<HealthComponent>("HealthComponent").Hit(damage);
        }
        if (body is Enemy)
        {
            (body as Enemy).KnockBack(GlobalPosition,knock * 1000);
        }
    }
}
