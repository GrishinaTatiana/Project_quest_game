using BloodTribute;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Hero : Character
{
    Item SelectedItem;
    HotbarItem SelectedHotbarItem;

    HScrollBar ScrollBar;
    HBoxContainer Hotbar;

    PackedScene hotbarItemScene = GD.Load<PackedScene>("res://Герой/UI/HotbarItem.tscn");

    public event Action FinishedInteracting;

    public List<IInteractable> GetIInteractableAreasUnderCursor()
    {
        var list = new List<IInteractable>();
        var spaceState = GetWorld2D().DirectSpaceState;
        var query = new PhysicsPointQueryParameters2D();
        query.Position = GetGlobalMousePosition();
        query.CollisionMask = 2;
        query.CollideWithAreas = true;
        var result = spaceState.IntersectPoint(query);
        foreach (var area in result)
        {
            var a = (Area2D)area["collider"];
            if (ReachArea.GetOverlappingAreas().Contains(a))
                if (a.GetParent() is IInteractable tmp)
                    list.Add(tmp);
        }
        return list;
    }

    public List<IInteractable> GetIInteractableOverlappingAreas()
    {
        var tmp = new List<IInteractable>();
        foreach(var e in ReachArea.GetOverlappingAreas())
        {
            if(e.GetParent() is IInteractable obj)
            {
                tmp.Add(obj);  
            }
        }
        return tmp;
    }

    public async void Interact(IInteractable interactable)
    {
        if (!interactable.CanInteract())
            ScuffedServiceProvider.GetService<IMessagePrinter>().PrintMessage(interactable.FailedInteraction);
        else
            await interactable.Interact(this);
        FinishedInteracting?.Invoke();
    }

    public override void _Ready()
    {
        ScrollBar = GetNode<HScrollBar>("CanvasLayer/ScrollBar");
        Hotbar = GetNode<HBoxContainer>("CanvasLayer/ScrollBar/HBoxContainer");
        InventoryChanged += UpdateHotbar;
        Puppeteer = new PlayerPuppeteer(this);
        base._Ready();
    }

    public void ChangedSelectedItem(HotbarItem item)
    {
        SelectedHotbarItem?.Unselect();
        item.Select();
        SelectedHotbarItem = item;
        SelectedItem = item._Item;
    }

    public void UpdateHotbar()
    {
        foreach(var e in Hotbar.GetChildren())
            e.QueueFree();

        foreach (var i in Inventory)
        {
            var tmp = hotbarItemScene.Instantiate<HotbarItem>();
            tmp.SetItem(i, () => ChangedSelectedItem(tmp));
            if(tmp._Item == SelectedItem)
            {
                tmp.Ready += tmp.Select;
                SelectedHotbarItem = tmp;
            }

            Hotbar.AddChild(tmp);
        }
    }
}
