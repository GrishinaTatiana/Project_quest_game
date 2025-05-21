using Godot;
using System;
using BloodTribute;

public partial class Level : Node2D
{
    public int BottomEdge;
    public override void _Ready()
    {
        var t = GetNode<Sprite2D>("Sprite2D");

        BottomEdge = (int)(t.GlobalPosition.Y + t.GetRect().Size.Y/2);

        var tmp = t.GenerateBoundaries();
        t.AddChild(tmp);

        base._Ready();
    }
}
