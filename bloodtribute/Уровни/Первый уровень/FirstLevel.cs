using Godot;
using System;

public partial class FirstLevel : Level
{
    [Export]
    public FirstDoor DoorToUnlock { get; set; }
    [Export]
    public FishesTank Fishes { get; set; }

    [Export]
    public MinigameInteractable MicrowaveMini { get; set; }

    [Export]
    public MinigameInteractable SafeMini { get; set; }

    public Door Door;



    public override void _Ready()
    {
        hero = GetNode<Hero>("Hero");
        Door = GetNode<Door>("Door");
        base._Ready();
        hero.Camera.LimitBottom = BottomEdge;
        hero.Camera.LimitLeft = LeftEdge;
        hero.Camera.LimitRight = RightEdge;
        hero.Camera.LimitTop = TopEdge;
    }

}
