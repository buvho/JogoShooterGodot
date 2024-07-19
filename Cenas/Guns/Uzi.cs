using Godot;

public partial class Uzi : Gun
{
    [Export]
    float Durantion;
    public override void berravior(PackedScene bulletsc)
    {
        Starter(Cooldown);
        ShootBullet(bulletsc);
    }
}
