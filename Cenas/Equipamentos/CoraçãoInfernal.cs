using Godot;
using System;

public partial class CoraçãoInfernal : Gear
{
     Node2D Particles;
    public override void Equip()
    {
    Particles = GetChild<CpuParticles2D>(0);
    Particles.Reparent(Player);
    Particles.Position = Vector2.Zero;
    Player.damX += 0.5;
    Player.GetNode<HealthComponent>("HealthComponent").HitX += 1;
    }
    public override void DesEquip()
    {
    Particles.Reparent(this);
    GetParent().GetParent<Player>().damX -= 0.5;
    GetParent().GetParent<Player>().GetNode<HealthComponent>("HealthComponent").HitX -= 1;
    }
}
