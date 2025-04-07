using Godot;
using BloodTribute;
using System;
using System.Threading;
using System.Threading.Tasks;

public partial class Door : Node2D, IInteractable
{
    public Area2D InteractableArea {  get; private set; }

    [Export]
    public Door ConnectedDoor;

    public event Action InteractionFinished;

    public async Task Interact(Character character)
    {
        character.GetParent().RemoveChild(character);
        ConnectedDoor.GetParent().AddChild(character);
        character.Position = ConnectedDoor.Position;
        InteractionFinished();
        await Task.Delay(1000);
        GD.Print("dsfdsfds");
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




}
