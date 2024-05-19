using Godot;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

public partial class Player : CharacterBody2D
{
    //movimentação
    public Vector2 Dash;
    public int VelConst = 500;
    public bool DashEnabled = true;
    public bool MousePressed = false;
    public bool automatic = false;

    //atributos
    public double damX = 1;
    public double maxhealth = 6;
    public int onHand = 0;
    public int coins = 0;

    //armas e itens
    public PackedScene[] guns = new PackedScene[2];
    [Export]
    PackedScene startergun;
    Gun arma;

    [Export]
    PackedScene itemSC;
    public Item itemCloseTo;

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

        if (Input.IsActionJustPressed("Dash") && DashEnabled == true)
        {
            Dash = mov.Normalized() * 130000;
            DashEnabled = false;
            GetNode<Timer>("DashTimer").Start(0.1);
        }
        
		Velocity = mov + (Dash * (float)delta);
		MoveAndSlide();
    
        if (mov == Vector2.Zero)
        {
        sprite.Play("stop");
        }
        else
        {
        sprite.Play("walk");
        }
             
        //rotation shenanigans
        var mouseAng = (GlobalPosition - GetGlobalMousePosition()).Angle();
        if (mouseAng > -1.5708 && mouseAng < 1.5708) 
        {
        sprite.Scale = new Vector2(1,1);
        }
        else 
        {
        sprite.Scale = new Vector2(-1,1);
        }

        //arma automatica
        if (automatic && MousePressed)
        {
            arma.Shoot();
        }
        arma.Target(GetGlobalMousePosition());
        
        if (MousePressed && arma.auto)
        {
            arma.Shoot(damX);
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
            if (KeyEvent.Keycode == Key.E && KeyEvent.Pressed && !KeyEvent.Echo && itemCloseTo != null)
            {
                if (itemCloseTo.IsGun)
                {
                    if(guns[1] == null)
                    {
                        guns[1] = itemCloseTo.item;
                        Change(1);
                    }
                    else
                    {
                        Item newitem = (Item)itemSC.Instantiate();
                        newitem.Position = Position;
                        newitem.item = guns[onHand];
                        GetTree().Root.GetNode<Node2D>("World").AddChild(newitem);
                        guns[onHand] = itemCloseTo.item;
                        Change(onHand);
                    }
                } else 
                {
                    GetNode<Node>("inventario").AddChild((Gear)itemCloseTo.item.Instantiate());
                }
                itemCloseTo.QueueFree();
            }
            if (KeyEvent.Pressed && !KeyEvent.Echo && KeyEvent.Keycode == Key.Q)//trocar de arma
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
    private void Change(int id)
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

