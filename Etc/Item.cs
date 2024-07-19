using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using System.Threading;
using Godot;

public partial class Item : Interactable
{
    [Export] 
    public PackedScene item;
    [Export]
    public bool costy;
    private int costw = 0;
    public bool IsGun = false;
    public override void _Ready()
    {
        Sprite2D s = GetNode<Sprite2D>("Sprite");
        GetNode<AnimationPlayer>("Animas").Play("Animas");
        if (item.Instantiate() is Gear eq)
        {
            s.Texture = eq.Texture;
            GetNode<Label>("Panel/V/nome").Text = $"{eq.Nome} ({eq.cost})";
            GetNode<Label>("Panel/V/descrição").Text = eq.Descrição;
            if (costy) Pricing(eq.cost);
        }

        else if (item.Instantiate() is Gun gun)
        {
            s.Texture = gun.SpriteFrames.GetFrameTexture("atira", 0);
            GetNode<Node2D>("Sprite").Scale = gun.Scale * (float)1.5;
            IsGun = true;
            GetNode<Label>("Panel/V/nome").Text = gun.Nome;
            GetNode<Label>("Panel/V/descrição").Text = gun.Descrição;
            if (costy) Pricing(gun.cost);
        }
    }
    public override void OnE(Player p)
    {
    if (costw <= p.coins)
    {
    p.CoinChange(-costw);
    if (item.Instantiate() is Gun)
        {
        if(p.guns[1] == null)
        {
            
            p.guns[1] = item;
            p.Change(1);
        }
        else
        {
            Item newitem = (Item)GD.Load<PackedScene>("res://Etc/Item.tscn").Instantiate();
            newitem.Position = p.Position;
            newitem.item = p.guns[p.onHand];
            GetTree().Root.GetNode<Node2D>("World").AddChild(newitem);
            newitem.Wee((GetGlobalMousePosition() - p.GlobalPosition).Normalized() * 10);
            p.guns[p.onHand] = item;
            p.Change(p.onHand);
        }
    } else if (item.Instantiate() is Gear gear)
    {
        p.GetNode<Node>("inventario").AddChild(gear);
    }
    QueueFree();
    }
    }

    private void Pricing(int cost)
    {
        costw = new Random().Next(5,15) + (cost * new Random().Next(8,12));
        GetNode<Control>("Price").Visible = true;
        GetNode<Label>("Price/Hbox/Label").Text = costw.ToString();
    }

    public void Wee(Vector2 vector)
    {
        var tweenbitches = CreateTween();
        tweenbitches.SetEase(Tween.EaseType.Out);
        tweenbitches.SetTrans(Tween.TransitionType.Quad);
        tweenbitches.TweenProperty(this,"position",Position + vector, 0.7f);
    }
}
