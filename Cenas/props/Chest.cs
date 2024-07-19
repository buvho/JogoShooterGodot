using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Chest : Interactable
{
    PackedScene itemSC;
    int R;
    Player pl;
    public override void OnE(Player p)
    {
        GetNode<AnimatedSprite2D>("Sprite").Frame = 1;
        Item drop = (Item)GD.Load<PackedScene>("res://Etc/Item.tscn").Instantiate();
        drop.item = Tudo.GetLoot();
        drop.Position = Position;
        GetParent().AddChild(drop);
        drop.Wee(new Vector2(0,25));
        Monitoring = false;
    }
}
