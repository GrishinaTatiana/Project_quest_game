using Godot;
using System;
using BloodTribute;

public partial class Level : Node2D
{
    public override void _Ready()
    {
        var t = GetNode<Sprite2D>("Sprite2D");


        var tmp = t.GenerateBoundaries();
        t.AddChild(tmp);

        base._Ready();
    }
}
