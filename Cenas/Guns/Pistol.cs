using Godot;

public partial class Pistol : Gun
{
    public override void berravior(PackedScene bulletsc)
    {
        Starter(Cooldown);
        ShootBullet(bulletsc);
    }
}
