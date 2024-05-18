using Godot;

public partial class Pistol : Gun
{
    public override void berravior(PackedScene bulletsc)
    {
        Starter(Cooldown);
        ShootBullet(bulletsc,10,0.1);
    }
}
