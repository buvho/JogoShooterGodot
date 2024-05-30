using Godot;
using System.Collections.Generic;

public partial class BalaMagica : Area2D
{
    private List<Enemy> enemies = new();
    bool search = true;

    public override void _Process(double delta)
    {
        if (search)
        {
            foreach(Enemy i in enemies)
            {
                GetNode<RayCast2D>("RayCast").TargetPosition = ToLocal(i.GlobalPosition);
                if(GetNode<RayCast2D>("RayCast").GetCollider() is Enemy)
                {
                    GetParent<Node2D>().Rotation = (GlobalPosition - i.GlobalPosition).Angle();
                    search = false;
                }
            }
        }
    }
    public void OnBody(Node2D Body)
    {
        if (Body is Enemy)
        {
        enemies.Add(Body as Enemy);
        }
    }
    public void OnExit(Node2D Body)
    {
        if (Body is Enemy)
        {
        enemies.Remove(Body as Enemy);
        }
    }
}   
