using Godot;
using System;

public partial class FirstQuest : Quest
{
    int counter;


    [Export]
    FirstDoor DoorToUnlock {  get; set; }

    [Export]
    Level FirstLevel { get; set; }

    [Export]
    OneTimeInteractable Counter {  get; set; }
    [Export]
    OneTimeInteractable Boots { get; set; }
    [Export]
    OneTimeInteractable Fishes { get; set; }

    public override void _Ready()
    {
        Counter.InteractionFinished += increaseCounter;
        Boots.InteractionFinished += increaseCounter;
        Fishes.InteractionFinished += increaseCounter;
    }

    void increaseCounter(IInteractable interactable)
    {
        interactable.InteractionFinished -= increaseCounter;
        counter++;
        if(counter == 3)
        {
            FinishQuest();
        }
    }

    protected override void FinishQuest()
    {
        DoorToUnlock.canInteract = true;
        FirstLevel.AddChild(GD.Load<PackedScene>("res://Предметы/test_key.tscn").Instantiate<TestKey>());
        base.FinishQuest();
    }
}
