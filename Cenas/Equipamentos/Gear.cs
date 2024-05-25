using Godot;

public partial class Gear : Sprite2D
{ 
    [Export]
    public int cost;
    [ExportGroup("iteminfo")]
    [Export]
    public string Nome;
    [Export]
    public string Descrição;
    public static Player Player;
    public override void _Ready()
    {
        Player = GetParent().GetParent<Player>();
    }
    public virtual void DesEquip(){}
    public virtual void Equip(){}
    public virtual void Passiva(){}
}
