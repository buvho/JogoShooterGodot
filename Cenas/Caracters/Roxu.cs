

public partial class Roxu : Enemy
{
    bool alive = true;
    public override void _Process(double delta)
    {
        if (alive)
        {
            Erotation();
            MoveToPlayer();
            TryShoot();
            Move(delta);
        }
    }
    public void Dead()
    {
        alive = false;
    }
}
