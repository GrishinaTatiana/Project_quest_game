using System;
using System.Threading.Tasks;
using BloodTribute;
using Godot;

public interface IInteractable
{
    public event Action<IInteractable> InteractionFinished;

    public Area2D InteractableArea { get; }

    public string ObjectName { get; }

    public Task Interact(Character character);

    public bool CanInteract();

    public string FailedInteraction {  get; }
}

