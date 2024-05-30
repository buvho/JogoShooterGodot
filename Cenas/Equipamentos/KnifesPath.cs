using Godot;

public partial class KnifesPath : Path2D
{
    public override void _Process(double delta)
    {
        foreach(PathFollow2D i in GetChildren())
        {
        HitBox h = i.GetNode<HitBox>("HitBox");
        Timer t = i.GetNode<Timer>("Timer");
        i.ProgressRatio += (float)(delta * 0.3);
            if (h.tocou == 1)
            {
                t.Start(3);
                h.tocou = 2;
                i.Visible = false;
                h.Monitoring = false;
            } else if (h.tocou == 2 && t.TimeLeft == 0)
            {
                h.Monitoring = true;
                i.Visible = true;
                h.tocou = 0;
            }
        }

    }
}
