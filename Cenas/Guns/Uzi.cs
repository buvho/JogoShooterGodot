using Godot;

public partial class Uzi : Gun
{
    [Export]
    float Durantion;
    public override void berravior(PackedScene bulletsc)
    {
        Starter(Cooldown);
        ShootBullet(bulletsc,1.0f,0.3);
    }
}
