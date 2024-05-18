using Godot;

public partial class Item : Sprite2D
{
    [Export] 
    public PackedScene item;
    public bool IsGun = false;

    public override void _Ready()
    {
        if (item.Instantiate() is Gun gun)
        {
        Texture = gun.itemTexture;
        Scale = gun.Scale * (float)1.5;
        IsGun = true;
        }
        else if (item.Instantiate() is Gear eq)
        {
            Texture = eq.Texture;
        }
    }
    public void OnBody(Node2D body)
    {
        if (body is Player p)
        {
           p.itemCloseTo = this;
        }
    }
        public void OnExit(Node2D body)
    {
        if (body is Player p && p.itemCloseTo == this)
        {
           p.itemCloseTo = null;
        }
    }
}
