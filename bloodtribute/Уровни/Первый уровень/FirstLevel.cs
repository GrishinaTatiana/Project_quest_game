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
}
