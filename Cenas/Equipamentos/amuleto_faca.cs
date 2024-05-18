
using Godot;

public partial class amuleto_faca : Gear 
{
    [Export]
    PackedScene facas;
    Node2D faca;
    public override void Equip()
    {
        faca = (Node2D)facas.Instantiate();
        GetTree().Root.GetNode("World/Player").AddChild(faca);
    }
    public override void DesEquip()
    {
        faca.QueueFree();
    }
}
