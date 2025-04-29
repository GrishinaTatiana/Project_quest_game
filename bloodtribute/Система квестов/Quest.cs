using Godot;
using System;
using BloodTribute;

public partial class Quest : Node
{
    public QuestStatus Status { get; private set; } = QuestStatus.ToBe;
    [Export]
    public string QuestName;

    public void StartQuest()
    {
        Status = QuestStatus.Active;
    }

    protected virtual void FinishQuest()
    {
        Status = QuestStatus.Finished;
        QuestManager.Instance.FinishQuest(this);
    }
}

public enum QuestStatus
{
    Active,
    Finished,
    ToBe
}