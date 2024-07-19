using Godot;

public partial class Shotgun : Gun
{
    [Export]
    float D = 1f;
       public override void berravior(PackedScene bulletsc)
    {
        Starter(Cooldown);
        ShootBullet(bulletsc);
        ShootBullet(bulletsc,0.3f);
        ShootBullet(bulletsc,-0.3f);
    }
}
