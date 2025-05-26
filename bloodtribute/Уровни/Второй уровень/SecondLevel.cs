using Godot;
using System;

public partial class SecondLevel : Level
{
    public Door Door { get; set; }

    public override void _Ready()
    {
        Door = GetNode<Door>("Door");

        base._Ready();
    }

}
