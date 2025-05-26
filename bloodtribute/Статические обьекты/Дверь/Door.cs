using Godot;
using BloodTribute;
using System;
using System.Threading;
using System.Threading.Tasks;

public partial class Door : Node2D, IInteractable
{
    public Area2D InteractableArea {  get; private set; }

    public string ObjectName => "Дверь";

    [Export]
    public string FailedInteraction { get; set; }

    [Export]
    public Node2D SpawnPoint;

    [Export]
    public Door ConnectedDoor;

    [Export]
    public AudioStreamPlayer audio { get; set; }

    public event Action<IInteractable> InteractionFinished;

    public async Task Interact(Character character)
    {
        var h = GetParent<Level>().Exit();
        ConnectedDoor.GetParent<Level>().Enter(h);
        h.GlobalPosition = ConnectedDoor.SpawnPoint.GlobalPosition;
        InteractionFinished?.Invoke(this);
        //await Task.Delay(1000);
    }

    public override void _Ready()
    {
        var area = GetNode<Area2D>("InteractableArea");
        if (area != null)
        {
            InteractableArea = area;
        }
        else
        {

        }


        base._Ready();
    }

    public virtual bool CanInteract()
    {
        return true;
    }
}
