using Godot;
using System;

public partial class DeskDrawerMini : MiniScreen
{
    Sprite2D openedDrawer;
    [Export] Item FishFood;

    public override void _Ready()
    {
        Background.GetChild<Button>(0).Pressed += openDrawer;

        openedDrawer = GetChild<Sprite2D>(1);
        openedDrawer.GetChild<Button>(0).Pressed += retriveFood;

        base._Ready();
    }

    void openDrawer()
    {
        Background.Hide();
        openedDrawer.Show();
        Background.GetChild<Button>(0).Pressed -= openDrawer;
    }

    void retriveFood()
    {
        openedDrawer.GetChild<Button>(0).Pressed -= retriveFood;
        openedDrawer.Hide();
        Hero.Instance.InsertItem(FishFood);
        FishFood = null;
        Background.Show();
        finishGame();
    }
}
