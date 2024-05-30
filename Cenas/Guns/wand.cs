using Godot;
using System;

public partial class wand : Gun
{
    public override void berravior(PackedScene bulletsc)
    {
        Starter(Cooldown);
        ShootBullet(bulletsc,1,0.1);
    }
}
