using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using Godot;

public partial class Level: Node2D
{
    [Export]
    PackedScene portaE;
    [Export]
    PackedScene portaD;
    [Export]
    PackedScene portaB;
    [Export]
    PackedScene portaC;
    PackedScene LevelSC;
    public List<Vector2> visinhos = new();
    public static List<Room> salas = new(); //objetos da sala
    List<Vector2> ocupados = new();
    int salasQ = 1;
    const int Tamanho = 496;

    public class Room
    {
        public Vector2 pos;
        public int profundidade;
        public List<Vector2> Parentes = new();
        public List<Vector2> Entradas = new();

        public Room(Vector2 pos,int profundidade)
        {
            this.pos = pos;
            this.profundidade = profundidade;
        }
    } 
    public override void _Ready()
    {
        salas.Add(new(new(0,0),0));

        salas[0].Entradas.Add(new(1, 0));
        salas[0].Entradas.Add(new(-1, 0));
        salas[0].Entradas.Add(new(0, 1));
        salas[0].Entradas.Add(new(0, -1));

        Room pai;
        int salasMax = 15;
        ocupados.Add(new(0,0)); //lista de coodernadas ja ocupadas


        while (salasQ <= salasMax)
        {

            do {
            int PaiNumero = new Random().Next(0,salas.Count); //escolhe um numero de uma sala para ser o pai
            pai = salas[PaiNumero];
            } while (pai.Entradas.Count == 0);
            visinhos.Clear();

            switch (new Random().Next(1,6))
            {
                case 1: 
                LevelSC = GD.Load<PackedScene>("res://Levels/level1.tscn");
                visinhos.Add(new(1, 0));
                visinhos.Add(new(-1, 0));
                visinhos.Add(new(0, 1));
                visinhos.Add(new(0, -1));
                break;

                case 2: LevelSC = GD.Load<PackedScene>("res://Levels/level2.tscn");
                visinhos.Add(new(1, 0));
                visinhos.Add(new(-1, 0));
                visinhos.Add(new(0, 1));
                visinhos.Add(new(0, -1));
                break;

                case 3: LevelSC = GD.Load<PackedScene>("res://Levels/level3.tscn");
                visinhos.Add(new(-1, 0));
                visinhos.Add(new(0, 1));
                break;

                case 4: LevelSC = GD.Load<PackedScene>("res://Levels/level4.tscn");
                visinhos.Add(new(0, 1));
                visinhos.Add(new(0, -1));
                break;
                
                case 5: LevelSC = GD.Load<PackedScene>("res://Levels/level5.tscn");
                visinhos.Add(new(1, 0));
                visinhos.Add(new(-1, 0));
                visinhos.Add(new(0, 1));
                visinhos.Add(new(0, -1));
                break;

                
            }
            
            Node2D level = (Node2D)LevelSC.Instantiate();
            Criar(pai,level);

        }



        for (int h = 0; h < 5;h++){
        visinhos.Clear();
        int prof = 0;
        Room bomgus = salas[0];
        foreach(Room i in salas)
        {
            if (prof < i.profundidade)
            {
                bomgus = i;
                prof = i.profundidade;
            }
        }
        visinhos.Add(new(1, 0));visinhos.Add(new(-1, 0));visinhos.Add(new(0, 1));visinhos.Add(new(0, -1));
        Criar(bomgus,(Node2D)GD.Load<PackedScene>("res://Levels/Sala do bau.tscn").Instantiate());
        bomgus.profundidade = 0;
        foreach(Vector2 i in visinhos)
        {
            Vector2 k = bomgus.pos + i;
            foreach(Room j in salas)
            {
                if (k == j.pos)
                {
                    j.profundidade = 0;
                    break;
                }
            }
        }
        }

        
        

        //logica de colcar as portas
        foreach (Room  i in salas)
        {
            if (!i.Parentes.Contains(i.pos + new Vector2(1, 0)) && i.Entradas.Contains( new Vector2(1, 0)))
            {
                Direita(i.pos);
            }
            if (!i.Parentes.Contains(i.pos + new Vector2(-1, 0)) && i.Entradas.Contains( new Vector2(-1, 0)))
            {
                Esquerda(i.pos);
            }
            if (!i.Parentes.Contains(i.pos + new Vector2(0, -1)) && i.Entradas.Contains( new Vector2(0, -1)))
            {
                Cima(i.pos);
            }
            if (!i.Parentes.Contains(i.pos + new Vector2(0, 1)) && i.Entradas.Contains( new Vector2(0, 1)))
            {
                Baixo(i.pos);
            }
        }
    }
    public void Esquerda(Vector2 rm)
    {
        StaticBody2D porrta = (StaticBody2D)portaE.Instantiate();
        porrta.Position = new Vector2(0,199) + rm  * Tamanho;
        AddChild(porrta);
    }
    public void Direita(Vector2 rm)
    {
        StaticBody2D porrta = (StaticBody2D)portaD.Instantiate();
        porrta.Position = new Vector2(464,201) + rm  * Tamanho;
        AddChild(porrta);
    }
    public void Cima(Vector2 rm)
    {
        StaticBody2D porrta = (StaticBody2D)portaC.Instantiate();
        porrta.Position = new Vector2(208,0) + rm  * Tamanho;
        AddChild(porrta);
    }
    public void Baixo(Vector2 rm)

    {
        StaticBody2D porrta = (StaticBody2D)portaB.Instantiate();
        porrta.Position = new Vector2(208,464) + rm  * Tamanho;
        AddChild(porrta);
    }
    public void Criar(Room pai,Node2D level)
    {
    for (int i = 0; i < 5;i++)
    {
        int N = new Random().Next(0,visinhos.Count);
        Vector2 localdofilho = pai.pos + visinhos[N]; 
        if (!ocupados.Contains(localdofilho) && pai.Entradas.Contains(visinhos[N])) 
        { 
            level.Position = localdofilho * Tamanho;
            AddChild(level);
            ocupados.Add(localdofilho);
            salasQ++;       
            salas.Add(new(localdofilho,pai.profundidade + 1));
            pai.Parentes.Add(salas[^1].pos); // ^1 = salas.count - 1
            salas[^1].Parentes.Add(pai.pos);
            foreach (Vector2 j in visinhos)
            {
                salas[^1].Entradas.Add(j * -1);
            }
            break;
        }
    }
    }
}
