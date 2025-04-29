using Godot;
using System;
using System.Collections.Generic;

public partial class QuestManager : Node
{
    Dictionary<Quest, List<Quest>> QuestQueue = new();
    public static QuestManager Instance { get; private set; }

    [Export]
    FirstQuest FirstQuest;

    public override void _Ready()
    {
        Instance = this;
        QuestQueue[FirstQuest] = new List<Quest>();
        FirstQuest.StartQuest();
    }

    public void FinishQuest(Quest quest)
    {
        GD.Print(quest.Name);
        foreach (var e in QuestQueue[quest])
            e.StartQuest();
    }

}
