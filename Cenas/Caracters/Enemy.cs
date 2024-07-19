using Godot;
using System;
using System.Security.Cryptography.X509Certificates;



public partial class Enemy : CharacterBody2D
{
    [Export]
    public float speed;
    [Export]
    float Weight;
    [Export]
    public float AtackRange;
    [Export]
    public float AntiRange;
    public bool alive = true;
    public Vector2 mov;
    public Vector2 knock = new();
    public Player playe;
    public AnimatedSprite2D sprite;
    public RayCast2D raycast;
    public PackedScene healSC;
    public PackedScene coinSC;

    public virtual void Tick(double delta){}
    public virtual void Tack(){}
    public virtual void Ded(){}

    public override void _Process(double delta)
    {
        if (alive){
        Tick(delta);
        }
        float knocke = Math.Abs(knock.X) + Math.Abs(knock.Y);
        if (knocke >= 0)
        { 
            knock *= Weight;
            knock = knocke < 0.1 ? Vector2.Zero : knock;
        }
    }
    public override void _Ready()
    {
        playe = GetTree().Root.GetNode("World").GetNode<Player>("Player");
        sprite = GetNode<AnimatedSprite2D>("Sprite");
        raycast = GetNode<RayCast2D>("Sprite/RayCast");
        coinSC = (PackedScene)GD.Load("res://Etc/Coin.tscn");
        healSC = (PackedScene)GD.Load("res://Etc/Heal.tscn");
        speed *= (float)new Random().Next(80,120) / 100;
        AtackRange *= (float)new Random().Next(80,120) / 100;
        AntiRange *= (float)new Random().Next(80,120) / 100;
        Tack();
    }

    public bool MoveToPlayer()
    {
        var PlayerAng = (GlobalPosition - playe.GlobalPosition).Angle();
        if (PlayerAng > -1.5708 && PlayerAng < 1.5708) 
        {
        sprite.Scale = new Vector2(0.7f,0.7f);
        }
        else 
        {
        sprite.Scale = new Vector2(-0.7f,0.7f);
        }

        raycast.TargetPosition = raycast.ToLocal(playe.GlobalPosition);
        if ((playe.GlobalPosition - GlobalPosition).Length() >= AntiRange * 10 || raycast.GetCollider() is not Player)
        {
        GetNode<NavigationAgent2D>("Nav").TargetPosition = playe.GlobalPosition;
        var direction = ToLocal(GetNode<NavigationAgent2D>("Nav").GetNextPathPosition()).Normalized();
        mov = direction * speed * 10;
        return true;
        }
        else
        {
            mov = Vector2.Zero;
            return false;
        }
    }

    public void KnockBack(Vector2 pos,float punch)
    {
        knock = (GlobalPosition - pos).Normalized() * punch;
    }
    public void Move(double delta)
    {
        Velocity = (mov + knock) * (float)delta;
        if (Velocity != Vector2.Zero){sprite.Play();}else{sprite.Stop();}
        MoveAndSlide();
    }

    public void Dead()
    {
        alive = false;
        SimpleLoot();
        Ded();
    }

    public void SimpleLoot()
    {
        Random RNG = new();
        int n = RNG.Next(1,11) + playe.coinL;
        int m = 11;
        int v = -10;
        while (n > 3){
            Coin coinz = (Coin)coinSC.Instantiate();
            CallDeferred(Node2D.MethodName.AddSibling, coinz);
            coinz.SetDeferred("position",Position + new Vector2(RNG.Next(v,m),RNG.Next(v,m)));
            n -= 3;
        }

        n = RNG.Next(1,11);
        if (n == 10)
        {
            Heal heal = (Heal)healSC.Instantiate();
            CallDeferred(Node2D.MethodName.AddSibling, heal);
            heal.SetDeferred("position",Position + new Vector2(RNG.Next(v,m),RNG.Next(v,m)));
        }
    }             
}