using Godot;
using System;

public partial class CoinUI : CanvasLayer
{
    private void OnCoinUI(int coins)
    {
        GetNode<Label>("Canvas/HBoxContainer/Label").Text = " " + coins + " ";
    }
}
