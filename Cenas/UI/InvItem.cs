using Godot;

public partial class InvItem : TextureButton
{
    bool equiped = false;
    static bool ocupied = false;
    public int ind;
    public Control pausemenu;
    public TextureRect NotchInv;   
    public TextureRect NotchEquip;
    InvItem copy;

    public override void _Ready()
    {
        pausemenu = GetTree().Root.GetNode<Control>("World/Player/Pause/Control");
        NotchEquip = pausemenu.GetNode<TextureRect>("PanelContainer/HBoxContainer/Notch");
        NotchInv = pausemenu.GetNode<TextureRect>("GridContainer/Notch");
        copy = (InvItem)GD.Load<PackedScene>("res://Cenas/UI/InvItem.tscn").Instantiate();
        copy.TextureNormal = TextureNormal;
        copy.Scale = Scale;

        if (GetParent() is HBoxContainer)
        {
        equiped = true;
        }
    }
    public void OnPressed()
    {
        if (!equiped && !ocupied) //caso o equipamento esteja no inventario com espaço disponivel
        {
            copy.Disabled = true;
            pausemenu.AddChild(copy);
            copy.GlobalPosition = GlobalPosition;
            Disabled = true;
            ind = GetIndex(); 
            (pausemenu.GetParent() as Pause).Mudanca(ind,true);
            copy.Indo();
            QueueFree();          
        }
        else if (ocupied && !equiped) //caso o equipamento esteja no inventario sem espaço disponivel
        {
            copy.Disabled = true;
            AddChild(copy);
            copy.GetNode<AnimationPlayer>("Animation").Play("NotGoing");
            GetNode<AnimationPlayer>("Animation").Play("blind");
            _Ready();
        }
        else if (ocupied && equiped) //caso o equipamento esteja equipado
        {
            copy.Disabled = true;
            pausemenu.AddChild(copy);
            copy.GlobalPosition = GlobalPosition;
            Disabled = true;
            ind = GetIndex(); 
            (pausemenu.GetParent() as Pause).Mudanca(ind,false);
            copy.Vindo();
            QueueFree();
        }
    }

    public void Indo() // ele vai
    {
        var tweenbitches = CreateTween();
        tweenbitches.TweenProperty(this,"position",NotchEquip.GlobalPosition,0.2);
        equiped = true;
        ocupied = true;
        copy.Disabled = false;
        GetNode<Timer>("Timer").Start(0.2);
    }

    public void Vindo() // ele volta
    {
        var tweenbitches = CreateTween();
        tweenbitches.TweenProperty(this,"position",NotchInv.GlobalPosition,0.2);
        equiped = false;
        ocupied = false;
        copy.Disabled = false;
        GetNode<Timer>("Timer").Start(0.2);
    }
    public void OnTimeOut()
    {
    if (!equiped && !ocupied) //se ele não ta equipado
    {
        copy.GlobalPosition = GlobalPosition;
        NotchInv.AddSibling(copy);
        NotchInv.MoveToFront();
        NotchInv.Visible = true;
        QueueFree();
    }
    else if (equiped && ocupied) //se ele ta equipado
    {
        copy.GlobalPosition = GlobalPosition;
        copy.equiped = true;
        NotchEquip.AddSibling(copy);
        NotchEquip.MoveToFront();
        NotchEquip.Visible = true;
        QueueFree();
    }
    }
}
