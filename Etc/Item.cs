using Godot;

public partial class Item : Interactable
{
    [Export] 
    public PackedScene item;
    public bool IsGun = false;
    public override void _Ready()
    {
        Sprite2D s = GetNode<Sprite2D>("Sprite");
        if (item.Instantiate() is Gun gun)
        {
            s.Texture = gun.itemTexture;
            GetNode<Node2D>("Sprite").Scale = gun.Scale * (float)1.5;
            IsGun = true;
            GetNode<Label>("Panel/V/nome").Text = gun.Nome;
            GetNode<Label>("Panel/V/descrição").Text = gun.Descrição;
        }
        else if (item.Instantiate() is Gear eq)
        {
            s.Texture = eq.Texture;
            GetNode<Label>("Panel/V/nome").Text = eq.Nome;
            GetNode<Label>("Panel/V/descrição").Text = eq.Descrição;
        }
    }
    public override void OnE(Player p)
    {
    if (IsGun)
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
    } else 
    {
        p.GetNode<Node>("inventario").AddChild((Gear)item.Instantiate());
    }
    QueueFree();
    }

    public void Wee(Vector2 vector)
    {
        var tweenbitches = CreateTween();
        tweenbitches.SetEase(Tween.EaseType.Out);
        tweenbitches.SetTrans(Tween.TransitionType.Quad);
        tweenbitches.TweenProperty(this,"position",Position + vector, 0.7f);
    }
}
