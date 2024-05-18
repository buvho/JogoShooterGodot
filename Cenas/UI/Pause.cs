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

        //mostra os itens no inventario
        foreach (Gear i in inventario.GetChildren().Cast<Gear>())
        {
            InvItem a = (InvItem)equip.Instantiate();
            a.TextureNormal = i.Texture;
            grid.AddChild(a);
        }
        foreach (Gear i in equipados.GetChildren().Cast<Gear>())
        {
            InvItem a = (InvItem)equip.Instantiate();
            a.TextureNormal = i.Texture;
            Container.AddChild(a);
        }
        grid.GetNode<CanvasItem>("Notch").MoveToFront();
        Container.GetNode<CanvasItem>("Notch").MoveToFront();
    }
    public void Mudanca(int index,bool indo)
    {
        if (indo){
        inventario.GetChild<Gear>(index).Equip();    
        inventario.GetChild(index).Reparent(equipados);
        }
        else {
        equipados.GetChild<Gear>(index).DesEquip();
        equipados.GetChild(index).Reparent(inventario);
        }
    }
}
