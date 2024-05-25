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
        pl = p;
        bool  c = true;
        while (c){
        R = new Random().Next(0,Tudo.loot.Count - 1);
        GD.Print(R);
        itemSC = GD.Load<PackedScene>(Tudo.loot[R]);
        Node2D iten = (Node2D)itemSC.Instantiate();
        if (iten is Gun)
        {
           c = Laction(p.guns.Contains(itemSC));
        }
        else if (iten is Gear)
        {
            bool ra = false;
            foreach(Gear i in p.GetNode("equipados").GetChildren().Cast<Gear>())
            {
                if(i.Nome == (iten as Gear).Nome) {ra = true;}
            }
            foreach(Gear i in p.GetNode("inventario").GetChildren().Cast<Gear>())
            {
                if(i.Nome == (iten as Gear).Nome) {ra = true;}
            }
           c = Laction(ra);
        }
    }
    }
    public bool Laction(bool test)
    {
        if (test)
        {
            Tudo.loot.RemoveAt(R);
            return true;
        } else
        {
            GetNode<AnimatedSprite2D>("Sprite").Frame = 1;
            Item drop = (Item)GD.Load<PackedScene>("res://Etc/Item.tscn").Instantiate();
            drop.item = itemSC;
            drop.Position = Position;
            GetTree().Root.GetNode("World").AddChild(drop);
            drop.Wee(new Vector2(0,25));
            Monitoring = false;
            return false;
        }
    }
}
