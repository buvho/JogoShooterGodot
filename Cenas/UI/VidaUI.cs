using Godot;

public partial class VidaUI : CanvasLayer
{
    float Size = 26;
    private void OnVidaUI(double vida)
    {
    TextureRect cora = GetNode<TextureRect>("Canvas/%Cora");
    cora.Size = new Vector2((float)(vida * Size * 0.5) , 24);
    }
}
