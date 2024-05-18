using Godot;
using System;



public partial class Enemy : CharacterBody2D
{
    [Export]
    float speed;
    [Export]
    float Weight;
    [Export]
    int ShootRange;
    [Export]
    public int Range;
    [Export]   
    PackedScene bulletsc;
    [Export]
    int SearchRange = 50;
    public Vector2 mov;
    public Vector2 knock = new();
    public CharacterBody2D playe;
    CharacterBody2D it;
    AnimatedSprite2D sprite;
    public RayCast2D raycast;
    public bool seen = false;
    public override void _Ready()
    {
        playe = GetTree().Root.GetNode("World").GetNode<CharacterBody2D>("Player");
        sprite = GetNode<AnimatedSprite2D>("Sprite");
        raycast = GetNode<RayCast2D>("RayCast");
    }

    public void SearchPlayer()
    {
        raycast.TargetPosition = ToLocal(playe.GlobalPosition);
            if (raycast.GetCollider() == playe && (playe.GlobalPosition - GlobalPosition).Length() <= Range * 50)
            {
                seen = true;
            }
    }
    public void Erotation()
    {
        var PlayerAng = (GlobalPosition - playe.GlobalPosition).Angle();
        if (PlayerAng > -1.5708 && PlayerAng < 1.5708) 
        {
        sprite.Scale = new Vector2(1,1);
        }
        else 
        {
        sprite.Scale = new Vector2(-1,1);
        }
    }
    public void TryShoot()
    {
        if ((playe.GlobalPosition - GlobalPosition).Length() <= ShootRange * 10)
        {
            if (raycast.GetCollider() == playe)
            {
                sprite.GetChild<Gun>(0).Target(playe.GlobalPosition);
                sprite.GetChild<Gun>(0).Shoot(bulletsc);
            }
        }
    }
    public void MoveToPlayer()
    {
        if ((playe.GlobalPosition - GlobalPosition).Length() <= Range * 10 && seen)
        {
        GetNode<NavigationAgent2D>("Nav").TargetPosition = playe.GlobalPosition;
        var direction = ToLocal(GetNode<NavigationAgent2D>("Nav").GetNextPathPosition()).Normalized();
        mov = direction * speed * 10;
        }
        else
        {
            mov = Vector2.Zero;
        }
    }

    public void KnockBack(Vector2 pos,float punch)
    {
        knock = (GlobalPosition - pos).Normalized() * punch;
    }
    public void Move(double delta)
    {
        float knocke = Math.Abs(knock.X) + Math.Abs(knock.Y);
        if (knocke >= 0)
        { 
            knock *= Weight;
            knock = knocke < 0.1 ? Vector2.Zero : knock;
        }
        Velocity = (mov + knock) * (float)delta;
        if (Velocity != Vector2.Zero){sprite.Play();}else{sprite.Stop();}
        MoveAndSlide();
    }             
}