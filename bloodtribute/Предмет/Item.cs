using Godot;
using System;
using System.Threading.Tasks;
using BloodTribute;

public partial class Item : Node2D, IInteractable
{
    
    public int ID {  get; private set; }

    public Area2D InteractableArea { get; private set; }
    public Sprite2D Sprite { get; private set; }
    [Export]
    public string ObjectName { get; private set; } // Надо как то по нормальнгому имплементировать систему ID

    [Export]
    public string FailedInteraction { get; set; }

    public Texture2D Icon { get { if (_Icon == null) return Sprite.Texture; else return _Icon; } }

    [Export]
    public AudioStreamPlayer audio { get; set; }

    [Export]
    public Texture2D _Icon;

    public event Action<IInteractable> InteractionFinished;

    public Item()
    {
        ID = GetId();
    }

    public override void _Ready()
    {
        InteractableArea = GetNode<Area2D>("InteractableArea");
        Sprite = GetNode<Sprite2D>("Sprite");
        if (InteractableArea!= null && InteractableArea.GetChildren().Count == 0)
            InteractableArea.AddChild(Sprite.GenerateShape());
    }

    public virtual Task Interact(Character character)
    {
        GetParent().RemoveChild(this);
        RemoveChild(InteractableArea);
        character.InsertItem(this);
        InteractionFinished?.Invoke(this);
        return Task.CompletedTask;
    }

    public bool CanInteract()
    {
        return true;
    }



    static int CurrentId = 0;

    static int GetId()
    {
        return CurrentId++;
    }









}
