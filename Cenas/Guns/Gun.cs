using System.Runtime.Serialization;
using Godot;

public partial class Gun : AnimatedSprite2D 
{
    double damX = 1;
    double damP = 0;
    public bool ready = true;
    [Export]
    PackedScene DefautBullet;

    [Export]
    public double damage;

    [Export]
    public float speed;

    [Export]
    public float Durantion;

    [Export]

    public float knockback;
    
    [Export]
    public float Cooldown;

    [Export]
    public double inacurace;

    [Export]
    public bool auto = false;

    [ExportGroup("iteminfo")]
    [Export]
    public string Nome;
    [Export]
    public string Descrição;
    [Export]
    public int cost;
    
    public Node2D BP;
    public virtual void berravior(PackedScene bulletsc)
    {

    }
    public void Shoot(double damageX = 1,double damageP = 0)
    {
        if (ready)
        {
            damX = damageX;
            damP = damageP;
            berravior(DefautBullet);
        }
    }
    public void Shoot(PackedScene bulletsc)
    {
        if (ready)
        {
            berravior(bulletsc);
        }
    }
    public void Target(Vector2 target)
    {
        GlobalRotation = (GlobalPosition - target).Angle();
    }

    public void Starter(float down)
    {
        BP = GetNode<Node2D>("BP");
        Play();
        GetNode<Timer>("Timer").Start(down);
        ready = false;
    }
    public void ShootBullet(PackedScene bulletsc, float angle = 0,float inaX = 0)
    {
        Bullet bullet = (Bullet)bulletsc.Instantiate();
        bullet.Position = BP.GlobalPosition;
        bullet.Rotation = BP.GlobalRotation + (float)GD.RandRange(-inacurace - inaX,inacurace + inaX) + angle ;
        bullet.Durantion = bullet.Durantion == 0 ? Durantion : bullet.Durantion;
        bullet.damage = bullet.damage == 0 ? damage : bullet.damage;
        bullet.knock = bullet.knock == 0 ? knockback : bullet.knock;
        bullet.speed = bullet.speed  == 0 ? speed : bullet.speed;
        bullet.damage += damP;
        bullet.damage *= damX;
        GetTree().Root.AddChild(bullet);
    }

    public void RayShoot()
    {
        var ray = GetNode<RayCast2D>("RayCast");
        if (ray.GetCollider() is CharacterBody2D)
        {
            var vitima = ray.GetCollider() as CharacterBody2D;
            vitima.GetNode<HealthComponent>("HealthComponent").Hit(damage * damX);
        }
        if (ray.GetCollider() is Enemy)
        {
            var vitima = ray.GetCollider() as Enemy;
            vitima.KnockBack(ray.GlobalPosition,knockback * 1000);
        }

    }

    public void OnTimeOut()
    {
        ready = true;
        Stop();
    }

}
