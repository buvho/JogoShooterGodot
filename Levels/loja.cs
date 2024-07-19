using Godot;
using System;
using System.Linq;

public partial class loja : Node2D
{    public override void _Ready()
    {
       foreach(Item i in GetNode<Node2D>("itens").GetChildren().Cast<Item>())
       {
        i.item = Tudo.GetLoot();
        i.costy = true;
        i._Ready();
       }
    }
}
