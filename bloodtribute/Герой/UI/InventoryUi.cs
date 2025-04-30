using Godot;
using System;
using System.Collections.Generic;

public partial class InventoryUi : PanelContainer
{
    [Export]
    public TextureButton Left;
    [Export]
    public TextureButton Right;
    [Export]
    public HBoxContainer Container;
    [Export]
    public Hero Hero;

    List<HotbarItem> Items = [];
    HotbarItem selectedHotBarItem;

    int inventoryOffset = 0;

    public override void _Ready()
    {
        foreach (var e in Container.GetChildren())
            if (e is HotbarItem item)
            {
                item.button.Pressed += () => ChangeSelection(item);
                Items.Add(item);
            }

        Left.Pressed += DecreaseOffset;
        Right.Pressed += IncreaseOffset;
    }

    void ChangeSelection(HotbarItem item)
    {
        if(selectedHotBarItem != null)
            selectedHotBarItem.Unselect();
        if (selectedHotBarItem != item)
        {
            selectedHotBarItem = item;
            Hero.ChangeSelectedItem(item._Item);
        }
        else
        {
            selectedHotBarItem = null;
            Hero.ChangeSelectedItem(null);
        }
    }

    public void UpdateInventory()
    {
        Item prevSelected = null;
        if (selectedHotBarItem != null)
        {
            prevSelected = selectedHotBarItem._Item;
            selectedHotBarItem.Unselect();
        }
        for(int i = 0; i < Items.Count; i++)
        {
            if (i + inventoryOffset < Hero.Inventory.Count)
            {
                Items[i].SetItem(Hero.Inventory[i + inventoryOffset]);
                if (Hero.Inventory[i + inventoryOffset] == prevSelected)
                {
                    selectedHotBarItem = Items[i];
                    Items[i].Select();
                }
            }
            else
                Items[i].SetItem();
        }
    }

    void IncreaseOffset() // Нужно в один метод но мне лень
    {
        if (inventoryOffset < Hero.Inventory.Count - 1 && Hero.Inventory.Count > Items.Count)
        {
            inventoryOffset++;
            UpdateInventory();
        }
    }

    void DecreaseOffset()
    {
        if (inventoryOffset > 0 && Hero.Inventory.Count > Items.Count)
        {
            inventoryOffset--;
            UpdateInventory();
        }
    }


}
