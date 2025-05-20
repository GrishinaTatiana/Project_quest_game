using Godot;
using System;

public partial class FirstLevel : Level
{
    [Export]
    public FirstDoor DoorToUnlock { get; set; }
    [Export]
    public OneTimeInteractable Counter { get; set; }
    [Export]
    public OneTimeInteractable Boots { get; set; }
    [Export]
    public OneTimeInteractable Fishes { get; set; }
}
