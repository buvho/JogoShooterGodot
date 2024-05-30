using Godot;
using System.Collections.Generic;
public partial class Player : CharacterBody2D
{
    //shenanigans
    public Vector2 Dash;
    public bool DashEnabled = true;
    public bool MousePressed = false;
    public int onHand = 0;

    //atributos
    public int VelConst = 650;
    public double damP = 0;
    public double damX = 1;
    public double maxhealth = 6;
    public int coins = 0;
    public int coinL = 0;
    public int NotchUse = 0;
    public int NotchLimit = 5;

    //armas e itens
    public PackedScene[] guns = new PackedScene[2];
    [Export]
    PackedScene startergun;
    Gun arma;
    public List<Interactable> CloseTo = new List<Interactable>();

    //eventos
    [Signal]
    public delegate void VidaUIEventHandler(double vida);
    [Signal]
    public delegate void ArmaUIEventHandler(Texture2D armaA, Texture2D armaB);
    public override void _Ready()
    {
        //configura arma inicial
        guns[0] = startergun;
        Gun guna = (Gun)startergun.Instantiate(); 
        guna.Position = GetNode<Node2D>("Sprite/Hand").Position;
        GetNode<Node2D>("Sprite/Hand").AddChild(guna);
    }
    public override void _Process(double delta)
    {
        arma = GetNode("Sprite/Hand").GetChild<Gun>(0);
        
        //movimento
        var sprite = GetNode<AnimatedSprite2D>("Sprite");
        Vector2 mov = Input.GetVector("left","right","up","down") * VelConst;
        sprite.SpeedScale = VelConst / 650;

        if (Input.IsActionJustPressed("Dash") && DashEnabled == true)
        {
            Dash = mov.Normalized() * 200000;
            DashEnabled = false;
            GetNode<Timer>("DashTimer").Start(0.1);
        }
        
		Velocity = mov + (Dash * (float)delta);
		MoveAndSlide();


        float scl = 0.8f;
        if (mov == Vector2.Zero)
        {
        sprite.Play("stop");
        }
        else
        {
        if ((Velocity.X > 0 && sprite.Scale.X == scl )||(Velocity.X < 0 && sprite.Scale.X == -scl ))
        {
        sprite.PlayBackwards("walk");
        } else {
        sprite.Play("walk");
        }
        }
             
        //rotation shenanigans
        var mouseAng = (GlobalPosition - GetGlobalMousePosition()).Angle();
        if (mouseAng > -1.5708 && mouseAng < 1.5708) 
        {
        sprite.Scale = new Vector2(scl,scl);
        }
        else 
        {
        sprite.Scale = new Vector2(-scl,scl);
        }

        //arma automatica
        arma.Target(GetGlobalMousePosition());
        
        if (MousePressed && arma.auto)
        {
            arma.Shoot(damX,damP);
        }
        
        //pause
        if (GetTree().Root.GetNode<Tudo>("Tudo").PauseState == 1)
        {
            CanvasLayer pause = (CanvasLayer)GD.Load<PackedScene>("res://Cenas/UI/Pause.tscn").Instantiate();
            AddChild(pause);
            GetTree().Paused = true;
            GetTree().Root.GetNode<Tudo>("Tudo").NoUI();
            GetTree().Root.GetNode<Tudo>("Tudo").PauseState = 2;
        }
        if (CloseTo.Count > 0)
        {
            CloseTo[0].GetNode<Node2D>("Sprite").Modulate = Color.Color8(150,200,255);
            if (CloseTo[0] is Item)
            {
                CloseTo[0].GetNode<Control>("Panel").Visible = true;
            }
        }

    }
    public override void _Input(InputEvent @event)
    {
       if (@event is InputEventMouseButton MouseEvent)
	   {
		if (MouseEvent.ButtonIndex == MouseButton.Left && MouseEvent.Pressed)
		{         
            arma.Shoot();
            MousePressed = true;
	    }
        if (MouseEvent.ButtonIndex == MouseButton.Left && !MouseEvent.Pressed)
        {
            MousePressed = false;
        }
       }
       if (@event is InputEventKey KeyEvent) //pegar item
       {
            if (KeyEvent.Keycode == Key.E && KeyEvent.Pressed && !KeyEvent.Echo && CloseTo.Count > 0)
            {
            CloseTo[0].OnE(this);
            if(CloseTo.Count > 0)
            {CloseTo.RemoveAt(0);}
            }
            if (KeyEvent.Pressed && !KeyEvent.Echo && KeyEvent.Keycode == Key.Q && arma.ready)//trocar de arma
            {
                if (onHand == 0 && guns[1] != null)
                {
                    Change(1);
                }
                else if (onHand == 1 && guns[0] != null)
                {
                    Change(0);
                }
            }
       }

    }
    private void DashTimeOut()
    {
        if (Dash != new Vector2(0,0))   
        {
            Dash = new Vector2(0,0);
            GetNode<Timer>("DashTimer").Start(0.5);
        }
        else if (DashEnabled == false)
        {
            DashEnabled = true;
        }

    }
    private void HealthChanged()
    {
        double vida = GetNode<HealthComponent>("HealthComponent").health;
        EmitSignal(SignalName.VidaUI, vida);
    }
    public void Change(int id)
    {

        foreach (Node2D i in GetNode<Node2D>("Sprite/Hand").GetChildren())
        {
            i.QueueFree();
        }
        onHand = id;
        Gun guna = (Gun)guns[id].Instantiate(); 
        guna.Position = GetNode<Node2D>("Sprite/Hand").Position;
        GetNode<Node2D>("Sprite/Hand").AddChild(guna);
        
        if (onHand == 0)
        {
            EmitSignal(SignalName.ArmaUI, ((Gun)guns[0].Instantiate()).itemTexture, ((Gun)guns[1].Instantiate()).itemTexture);
        }
        else 
        {
            EmitSignal(SignalName.ArmaUI, ((Gun)guns[1].Instantiate()).itemTexture, ((Gun)guns[0].Instantiate()).itemTexture);
        }
    }
    public void CoinChange(int amount)
    {
        coins += amount;
        GetNode<Label>("CoinUI/Canvas/HBoxContainer/Label").Text = " " + coins + " ";
    }
}

