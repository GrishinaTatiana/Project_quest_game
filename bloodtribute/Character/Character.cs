using Godot;
using System;

public partial class Character : CharacterBody2D
{
    public AnimatedSprite2D Sprite { get; private set; }
    public Area2D ReachArea { get; private set; }
    [Export]
    public float Speed { get; set; }

    [Export]
    public float Gravity { get; set; }

    public override void _Ready()
    {
        Sprite = GetNode<AnimatedSprite2D>("Sprite");
        ReachArea = GetNode<Area2D>("ReachArea");

        base._Ready();
    }
}
