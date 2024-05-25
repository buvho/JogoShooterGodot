using Godot;
using System;

public partial class BalaChumbo : Gear
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
