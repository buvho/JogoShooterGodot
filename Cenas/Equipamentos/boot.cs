

public partial class boot : Gear
{
public override void Equip()
{
    GetParent().GetParent<Player>().damP += 1;
}

public override void DesEquip()
{
    GetParent().GetParent<Player>().damP -= 1;
}
}
