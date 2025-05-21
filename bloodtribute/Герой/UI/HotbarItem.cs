using Godot;
using System;

public partial class HotbarItem : Control //Говнище
{
    TextureRect Background;
    TextureRect Selected;
    TextureRect ItemIcon;
    public Button button;

    Texture2D ItemTexture;

    bool changeOnToggle = true;
    bool toggled = false;
    public Item _Item {  get; private set; }


    public void SetItem(Item item)
    {
        _Item = item;
        ItemIcon.Texture = item.Icon;
    }
    public void SetItem()
    {
        _Item = null;
        ItemIcon.Texture = null;
    }

    public override void _Ready()
    {
        Background = GetNode<TextureRect>("Background");
        Selected = GetNode<TextureRect>("Selected");
        ItemIcon = GetNode<TextureRect>("ItemIcon");
        button = GetNode<Button>("Button");

        button.MouseEntered += ToggleOn;
        button.MouseExited += ToggleOff;
        button.Pressed += ToggleSelection;
    }

    void ToggleOn()
    {
        if (changeOnToggle)
            Selected.Visible = true;
    }
    void ToggleOff()
    {
        if(changeOnToggle)
            Selected.Visible = false;
    }

    void ToggleSelection()
    {
        if (toggled)
            Unselect();
        else
            Select();
    }

    public void Select()
    {
        toggled = true;
        Selected.Visible = true;
        changeOnToggle = false;
    }

    public void Unselect()
    {
        toggled = false;
        Selected.Visible = false;
        changeOnToggle = true;
    }
}
