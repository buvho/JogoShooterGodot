using Godot;

public partial class Interactable : Area2D
{
    public void OnBody(Node2D body)
    {
        if (body is Player p && !p.CloseTo.Contains(this))
        {
           p.CloseTo.Add(this);
        }
    }
    public void OnExit(Node2D body)
    {
        if (body is Player p && p.CloseTo.Count > 0 && p.CloseTo[0] == this)
        {
            GetNode<Node2D>("Sprite").Modulate = Color.Color8(255,255,255);
            p.CloseTo.Remove(this);
            if (this is Item)
            {
            GetNode<Control>("Panel").Visible = false;
            }
        }
    }

    public virtual void OnE(Player p){}
}
