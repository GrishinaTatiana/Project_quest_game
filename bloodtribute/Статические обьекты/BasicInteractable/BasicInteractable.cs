using Godot;
using System;
using System.Threading.Tasks;
using BloodTribute;

public partial class BasicInteractable : Node2D, IInteractable
{
    [Export]
    public Area2D InteractableArea { get; set; }

    [Export]
    public string ObjectName { get; set; }
    [Export] 
    public string InteractionText { get; set; }

    [Export]
    public Sprite2D Sprite { get; set; }

    [Export]
    public string FailedInteraction { get; set; }

    [Export]
    public AudioStreamPlayer audio { get; set; }

    public event Action<IInteractable> InteractionFinished;

    public override void _Ready()
    {
        if(InteractableArea.GetChildren().Count == 0)
        {
            var tmp = Sprite.GenerateShape();
            InteractableArea.AddChild(tmp);
        }
    }

    public virtual async Task Interact(Character character)
    {
        ScuffedServiceProvider.GetService<IMessagePrinter>().PrintMessage(InteractionText);
        InteractionFinished?.Invoke(this);
    }

    public virtual bool CanInteract()
    {
        return true;
    }
}
