using Godot;
using System;

public partial class HotbarItem : Control //Говнище
{
    TextureRect Background;
    TextureRect Selected;
    TextureRect ItemIcon;
    public Button button;

    Texture2D ItemTexture;
    Action sdfdssd;

    bool changeOnToggle = true;
    public Item _Item {  get; private set; }


    public void SetItem(Item item, Action action)
    {
        _Item = item;
        sdfdssd = action;
        ItemTexture = item.Sprite.Texture;
    }

    public override void _Ready()
    {
        Background = GetNode<TextureRect>("Background");
        Selected = GetNode<TextureRect>("Selected");
        ItemIcon = GetNode<TextureRect>("ItemIcon");
        button = GetNode<Button>("Button");
        ItemIcon.Texture = ItemTexture;

        button.MouseEntered += ToggleOn;
        button.MouseExited += ToggleOff;
        button.Pressed += sdfdssd;
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

    public void Select()
    {
        Selected.Visible = true;
        changeOnToggle = false;
    }

    public void Unselect()
    {
        Selected.Visible = false;
        changeOnToggle = true;
    }
}
