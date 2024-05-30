using Godot;

public partial class Shotgun : Gun
{
    [Export]
    float D = 1f;
       public override void berravior(PackedScene bulletsc)
    {
        Starter(Cooldown);
        ShootBullet(bulletsc,D,0.1f);
        ShootBullet(bulletsc,D,0.1f,0.3f);
        ShootBullet(bulletsc,D,0.1f,-0.3f);
    }
}
