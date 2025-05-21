using BloodTribute;
using Godot;
using System;

public partial class FirstDoor : Door
{
    public bool canInteract = false;

    [Export]
    public string cantleavetext;

    public override bool CanInteract()
    {
        if(canInteract)
            return canInteract;
        else
        {
            ScuffedServiceProvider.GetService<IMessagePrinter>().PrintMessage(cantleavetext);
            return false;
        }
    }
}
