using System;
using System.Threading.Tasks;
using BloodTribute;
using Godot;

public interface IInteractable
{
    public event Action InteractionFinished;

    public Area2D InteractableArea { get; }

    public Task Interact(Character character);
}

