

public partial class Roxu : ShootingEnemy
{
    public override void Tick(double delta)
    {
    MoveToPlayer();
    TryShoot();
    Move(delta);
    }
}
