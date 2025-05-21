using Godot;
using System;
using System.Threading.Tasks;
using BloodTribute;


public partial class FishesTank : BasicInteractable
{
    public bool BeenInteracted { get; set; }

    [Export]
    public string AlreadyInteractedText;

    [Export]
    public string requiredItemName;

    public override bool CanInteract()
    {
        if (BeenInteracted)
        {
            ScuffedServiceProvider.GetService<IMessagePrinter>().PrintMessage(AlreadyInteractedText);
            return false;
        }
        if(Hero.Instance.SelectedItem != null && Hero.Instance.SelectedItem.ObjectName == requiredItemName)
            return true;
        else
        {
            ScuffedServiceProvider.GetService<IMessagePrinter>().PrintGenericFailedMsg();
            return false;
        }
    }

    public override Task Interact(Character character)
    {
        BeenInteracted = true;
        return base.Interact(character);
    }
}
