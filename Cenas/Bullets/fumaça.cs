using Godot;

public partial class fumaça : Sprite2D
{
    public override void _Ready()
    {
        var tweenbitches = CreateTween();
        tweenbitches.TweenProperty(this,"modulate:a",0,1);
    }
}
