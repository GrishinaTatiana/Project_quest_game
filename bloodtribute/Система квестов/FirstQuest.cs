using Godot;
using System;

public partial class FirstQuest : Quest
{
    int counter;
    [Export]
    FirstLevel FirstLevel { get; set; }

    public override void _Ready()
    {
        FirstLevel.Counter.InteractionFinished += increaseCounter;
        FirstLevel.Boots.InteractionFinished += increaseCounter;
        FirstLevel.Fishes.InteractionFinished += increaseCounter;
    }

    void increaseCounter(IInteractable interactable)
    {
        interactable.InteractionFinished -= increaseCounter;
        counter++;
        if(counter == 1)
        {
            FinishQuest();
        }
    }

    protected override void FinishQuest()
    {
        FirstLevel.DoorToUnlock.canInteract = true;
        for (int i = 0; i < 7; i++)
        {
            FirstLevel.AddChild(GD.Load<PackedScene>("res://Предметы/test_key.tscn").Instantiate<TestKey>());
            FirstLevel.AddChild(GD.Load<PackedScene>("res://Предмет/Item.tscn").Instantiate<Item>());
        }
        base.FinishQuest();
    }
}
