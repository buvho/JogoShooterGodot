using Godot;
using System;

public partial class BalaOuro : Gear
{
public override void Equip()
{
    GetParent().GetParent<Player>().coinL += 3;
}

public override void DesEquip()
{
    GetParent().GetParent<Player>().coinL -= 3;
}
}
