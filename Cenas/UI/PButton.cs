using Godot;

public partial class PButton : Button
{
    public void OnPressed()
    {
        Despause();
    }

    public override void _Process(double delta)
    {
        if (GetTree().Root.GetNode<Tudo>("Tudo").PauseState == 3)
        {
            Despause();
        }
    }
    public void Despause()
    {
        GetTree().Paused = false;
        GetOwner<CanvasLayer>().QueueFree();
        GetTree().Root.GetNode<Tudo>("Tudo").BackUI();
        GetTree().Root.GetNode<Tudo>("Tudo").PauseState = 0;
    }
}
