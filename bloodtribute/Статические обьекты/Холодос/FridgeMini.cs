using Godot;
using System;

public partial class FridgeMini : MiniScreen
{
    Sprite2D openedDoor;
    [Export] Item Breakfast;

    public override void _Ready()
    {
        Background.GetChild<Button>(0).Pressed += openDoor;

        openedDoor = GetChild<Sprite2D>(1);
        openedDoor.GetChild<Button>(0).Pressed += retriveMeal;

        base._Ready();
    }

    void openDoor()
    {
        Background.Hide();
        openedDoor.Show();
        Background.GetChild<Button>(0).Pressed -= openDoor;
    }

    void retriveMeal()
    {
        openedDoor.GetChild<Button>(0).Pressed -= retriveMeal;
        openedDoor.Hide();
        Hero.Instance.InsertItem(Breakfast);
        Breakfast = null;
        Background.Show();
        finishGame();
    }
}
