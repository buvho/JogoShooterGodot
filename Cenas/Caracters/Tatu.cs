using Godot;
public partial class Tatu : Enemy
{
    [Export]
    int inicialvel;
    [Export]
    float acel;
    public enum states
    {
        indo,
        mira,
        ataque,
        espera
    }
    states state = states.indo;
    Vector2 aim;
    double timer = 1;
    public override void Tick(double delta)
    {
        switch (state)
        {
        case states.indo:
        bool b = MoveToPlayer();
        Move(delta);
        if (!b){
        aim = (playe.GlobalPosition - GlobalPosition).Normalized();
        timer = 1;
        sprite.Play("enchuga");
        GetNode<CollisionShape2D>("Collision").Scale = new Vector2(0.6f,0.5f);
        GetNode<CollisionShape2D>("Collision").Position = new Vector2(0,2.5f);
        state = states.mira;
        };
        break;

        case states.mira:
        timer -= delta;
        if (sprite.Frame == 5)
        {
            sprite.Play("giro");
        }
        if (timer > 0)
        {
            Velocity = -aim * 40;
        } else 
        {
            Velocity = aim * inicialvel;
            state = states.ataque;
        }
        MoveAndSlide();
        break;

        case states.ataque:
        if (Velocity.Length() < 4000)
        {
        Velocity += aim * acel;
        }
        MoveAndSlide();
        if (IsOnWall())
        {
        timer = 1;
        state = states.espera;
        sprite.PlayBackwards("enchuga");
        GetNode<CollisionShape2D>("Collision").Scale = new Vector2(1f,1f);
        GetNode<CollisionShape2D>("Collision").Position = Vector2.Zero;
        }
        break;

        case states.espera:
        Velocity = Vector2.Zero;
        timer -= delta;
        if (timer <= 0)
        {
            state = states.indo;
            sprite.Play("default");
        }
        break;
        }
    }
    public void OnBody(Node2D Body)
    {
        if (Body is Player)
        {
            playe.GetNode<HealthComponent>("HealthComponent").Hit(1);
            playe.Dash = (playe.GlobalPosition - GlobalPosition).Normalized() * 170000;
        }
    }
}

