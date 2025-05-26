using Godot;
using System;

public partial class TestScene : Node
{
    public override void _Ready()
    {
        var l1 = GetChild<FirstLevel>(2);
        var l2 = GetChild<SecondLevel>(3);

        l1.Door.ConnectedDoor = l2.Door;
        l2.Door.ConnectedDoor = l1.Door;


        base._Ready();
    }
}
