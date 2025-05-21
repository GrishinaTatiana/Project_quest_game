using BloodTribute;
using Godot;
using System;
using System.Threading.Tasks;

public partial class OneTimeInteractable : BasicInteractable
{
    public bool BeenInteracted { get; set; }


    public override bool CanInteract()
    {
        return !BeenInteracted;
    }

    public override Task Interact(Character character)
    {
        BeenInteracted = true;
        return base.Interact(character);
    }
}
