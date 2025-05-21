using Godot;
using System;

public partial class FirstQuest : Quest
{
    int counter;
    [Export]
    FirstLevel FirstLevel { get; set; }

    public override void _Ready()
    {
        FirstLevel.SafeMini.SolvedGame += increaseCounter;
        FirstLevel.MicrowaveMini.SolvedGame += increaseCounter;
        FirstLevel.Fishes.InteractionFinished += (j) => increaseCounter();
    }

    void increaseCounter()
    {
        counter++;
        if(counter == 3)
        {
            FinishQuest();
        }
    }

    protected override void FinishQuest()
    {
        FirstLevel.DoorToUnlock.canInteract = true;
        /*
        for (int i = 0; i < 7; i++)
        {
            FirstLevel.AddChild(GD.Load<PackedScene>("res://Предметы/test_key.tscn").Instantiate<TestKey>());
            FirstLevel.AddChild(GD.Load<PackedScene>("res://Предмет/Item.tscn").Instantiate<Item>());
        }*/
        base.FinishQuest();
    }
}
