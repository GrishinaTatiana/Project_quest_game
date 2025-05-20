using Godot;
using System;
using System.Threading.Tasks;

public partial class MinigameInteractable : BasicInteractable
{
    [Export]
    string miniScreenPath { get; set; }

    MiniScreen miniScreen { get; set; }

    public override void _Ready()
    {
        miniScreen = GD.Load<PackedScene>(miniScreenPath).Instantiate<MiniScreen>();
        base._Ready();
    }

    public override async Task Interact(Character character)
    {
        AddChild(miniScreen);
        await ToSignal(miniScreen, "Exit");
        RemoveChild(miniScreen);
        base.Interact(character);
    }

}
