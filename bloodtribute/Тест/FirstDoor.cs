using Godot;
using System;

public partial class FirstDoor : Door
{
    public bool canInteract = false;

    public override bool CanInteract()
    {
        return canInteract;
    }
}
