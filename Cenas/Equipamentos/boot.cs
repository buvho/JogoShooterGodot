

public partial class boot : Gear
{
public override void Equip()
{
    GetParent().GetParent<Player>().VelConst += 325;
}

public override void DesEquip()
{
    GetParent().GetParent<Player>().VelConst -= 325;
}
}
