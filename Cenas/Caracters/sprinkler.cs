using Godot;
using System;

public partial class sprinkler : Enemy
{
    [Export]
    PackedScene bulletSC;
    bool tiktak = false;

    public override void Tack()
    {
        sprite.Play("default");
    }
    public override void Tick(double delta)
    {
        if (sprite.Frame == 1 && tiktak)
        {
            Shoot(true);
            tiktak = false;
        }
        else if (sprite.Frame == 5 && !tiktak)
        {
            tiktak = true;
            Shoot(false);
        }
    }
    private void Shoot(bool T)
    {
        Node target;
        if (!T)
        {
            target = GetNode("RotX");
        } else
        {
            target = GetNode("RotT");

        }
        foreach(Node2D i in target.GetChildren())
        {
            Bullet b = (Bullet)bulletSC.Instantiate();
            b.Rotation = i.Rotation;
            b.Position = ToGlobal(i.Position);
            b.Durantion = 100;
            GetTree().Root.AddChild(b);
            GD.Print();
        }
    }
}
