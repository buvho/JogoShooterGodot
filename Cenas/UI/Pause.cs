using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Godot;
public partial class Pause : CanvasLayer
{
    [Export]
    PackedScene equip;
    Node equipados;
    Node inventario;
    public List<Gear> eq = new List<Gear>();

    public override void _Ready()
    {

        GridContainer grid = GetNode<GridContainer>("Control/GridContainer");
        HBoxContainer Container = GetNode<HBoxContainer>("Control/PanelContainer/HBoxContainer");
        equipados = GetParent<Player>().GetNode<Node>("equipados");
        inventario = GetParent<Player>().GetNode<Node>("inventario");
        GetNode<TextureRect>("Control/BackNotch").SetSize(new Vector2(8 * GetParent<Player>().NotchLimit,8));

        //mostra os itens no inventario
        foreach (Gear i in inventario.GetChildren().Cast<Gear>())
        {
            InvItem a = (InvItem)equip.Instantiate();
            a.TextureNormal = i.Texture;
            a.cost = i.cost;
            grid.AddChild(a);
        }
        foreach (Gear i in equipados.GetChildren().Cast<Gear>())
        {
            InvItem a = (InvItem)equip.Instantiate();
            a.TextureNormal = i.Texture;
            a.cost = i.cost;
            Container.AddChild(a);
        }
        grid.GetNode<CanvasItem>("Notch").MoveToFront();
        Container.GetNode<CanvasItem>("Notch").MoveToFront();
        GetNode<TextureRect>("Control/FrontNotch").SetSize(new Vector2(8 * GetParent<Player>().NotchUse,8));
    }
    public void Mudanca(int index,bool indo)
    {
        if (indo){
        GetParent<Player>().NotchUse += inventario.GetChild<Gear>(index).cost;
        inventario.GetChild<Gear>(index).Equip();    
        inventario.GetChild(index).Reparent(equipados);
        GetNode<TextureRect>("Control/FrontNotch").SetSize(new Vector2(8 * GetParent<Player>().NotchUse,8));
        }
        else {
        GetParent<Player>().NotchUse -= equipados.GetChild<Gear>(index).cost;
        equipados.GetChild<Gear>(index).DesEquip();
        equipados.GetChild(index).Reparent(inventario);
        GetNode<TextureRect>("Control/FrontNotch").SetSize(new Vector2(8 * GetParent<Player>().NotchUse,8));
        }
    }

}
