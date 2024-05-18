using Godot;

public partial class Shotgun : Gun
{
    [Export]
    double D = 1.5;
       public override void berravior(PackedScene bulletsc)
    {
        Starter(Cooldown);
        ShootBullet(bulletsc,(float)D);
        ShootBullet(bulletsc,(float)D,(float)0.3);
        ShootBullet(bulletsc,(float)D,(float)-0.3);
    }
}
