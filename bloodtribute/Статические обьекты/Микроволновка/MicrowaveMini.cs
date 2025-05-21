using Godot;
using System;
using System.Threading.Tasks;

public partial class MicrowaveMini : MiniScreen
{
    Sprite2D openedDoor;
    Sprite2D openedDoorWFood;
    Sprite2D closedDoorWFood;


    public override void _Ready()
    {
        Background.GetChild<Button>(0).Pressed += openDoor;

        openedDoor = GetChild<Sprite2D>(1);
        openedDoor.GetChild<Button>(0).Pressed += placeMeal;

        openedDoorWFood = GetChild<Sprite2D>(2);
        openedDoorWFood.GetChild<Button>(0).Pressed += retriveMeal;
        closedDoorWFood = GetChild<Sprite2D>(3);

        base._Ready();
    }

    void openDoor()
    {
        Background.Hide();
        openedDoor.Show();
        Background.GetChild<Button>(0).Pressed -= openDoor;
    }

    async void placeMeal()
    {
        if(Hero.Instance.SelectedItem == null || Hero.Instance.SelectedItem.ObjectName != "Завтрак")
            return;
        Hero.Instance.RemoveItem(Hero.Instance.SelectedItem);
        openedDoor.GetChild<Button>(0).Pressed -= placeMeal;
        openedDoor.Hide();
        closedDoorWFood.Show();
        await Task.Delay(1000);
        closedDoorWFood.Hide();
        openedDoorWFood.Show();
    }

    void retriveMeal()
    {
        openedDoorWFood.GetChild<Button>(0).Pressed -= retriveMeal;
        openedDoorWFood.Hide();
        openedDoor.Show();
        finishGame();
    }



}
