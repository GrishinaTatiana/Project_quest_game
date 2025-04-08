using Godot;
using System;
using System.Threading.Tasks;
using BloodTribute;

public partial class Item : Node2D, IInteractable
{
    public Area2D InteractableArea { get; private set; }
    public Sprite2D Sprite { get; private set; }
    [Export]
    public string ObjectName {  get; private set; }

    public event Action InteractionFinished;

    public override void _Ready()
    {
        InteractableArea = GetNode<Area2D>("InteractableArea");
        Sprite = GetNode<Sprite2D>("Sprite");
        if (InteractableArea.GetChildren().Count == 0)
            InteractableArea.AddChild(Sprite.GenerateShape());
    }

    public virtual Task Interact(Character character)
    {
        GetParent().RemoveChild(this);
        RemoveChild(InteractableArea);
        character.InsertItem(this);
        InteractionFinished?.Invoke();
        return Task.CompletedTask;
    }



}
