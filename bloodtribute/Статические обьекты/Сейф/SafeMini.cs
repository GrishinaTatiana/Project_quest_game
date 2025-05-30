using Godot;
using System;

public partial class SafeMini : MiniScreen
{
    [Export]
    public string Password;

    [Export] Item Gun;
    [Export] Item ID;
    [Export] AudioStreamPlayer AudioOpen;
    [Export] AudioStreamPlayer AudioTake;
    [Export] AudioStreamPlayer AudioButton;


    Sprite2D OpenedSprite;
    Sprite2D EmptySprite;

    string currentBuffer = "";

    public override void _Ready()
    {
        OpenedSprite = GetChild<Sprite2D>(2);
        OpenedSprite.GetChild<Button>(0).Pressed += grabItems;

        EmptySprite = GetChild<Sprite2D>(3);

        var child = GetChild(0);

        for (int i = 0; i < 10; i++)
        {
            int current = i;
            child.GetChild<Button>(i).Pressed += () =>
            {
                addNumber(current);
                AudioButton.Play();
            };

        }

        child.GetChild<Button>(10).Pressed += () =>

        {
            AudioButton.Play();
            if (currentBuffer.Equals(Password)) openSafe();
        };
        child.GetChild<Button>(11).Pressed += () => {
            AudioButton.Play();
            currentBuffer = "";
        };

        base._Ready();
    }

    void addNumber(int number)
    {
        if (currentBuffer.Length < 4)
            currentBuffer = currentBuffer + number;
    }

    void openSafe()
    {
        Background.Hide();
        OpenedSprite.Show();
        AudioOpen.Play();
    }

    void grabItems()
    {
        AudioTake.Play();
        OpenedSprite.Hide();
        EmptySprite.Show();
        Hero.Instance.InsertItem(Gun);
        Hero.Instance.InsertItem(ID);
        Gun = null;
        ID = null;
        finishGame();
    }

}