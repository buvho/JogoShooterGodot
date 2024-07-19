using Godot;

public partial class Sniper : Gun
{
    [Export]
    PackedScene fumaça;
    public override void berravior(PackedScene bulletsc)
    {
        var raycast = GetNode<RayCast2D>("RayCast");
        Starter(Cooldown);
        RayShoot();
        var FumasCalcs = raycast.GetCollisionPoint() - raycast.GlobalPosition;
        Sprite2D fumas = (Sprite2D)fumaça.Instantiate();
        fumas.Rotation = GlobalRotation;
        fumas.Position = raycast.GlobalPosition;
        fumas.Position += FumasCalcs / 2;
        Vector2 memes = new Vector2(FumasCalcs.Length(),2);
        fumas.Scale = memes;
        GetTree().Root.AddChild(fumas);
    }
}
