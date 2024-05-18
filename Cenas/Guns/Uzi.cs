using Godot;

public partial class Uzi : Gun
{
    public override void berravior(PackedScene bulletsc)
    {
        Starter(Cooldown);
        ShootBullet(bulletsc,2,0.3);
    }
}
