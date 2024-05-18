using Godot;

public partial class ArmasUI : CanvasLayer
{
    private void OnArmaUI(Texture2D ArmaA, Texture2D ArmaB)
    {
        GetNode<TextureRect>("armas/ArmaA").Texture = ArmaA;
        GetNode<TextureRect>("armas/ArmaB").Texture = ArmaB;
    }
}
