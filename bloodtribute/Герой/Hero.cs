using BloodTribute;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Hero : Character
{
    public Item SelectedItem { get; set; }
    [Export]
    InventoryUi InventoryUi { get; set; }

    public static Hero Instance;

    Camera2D Camera;

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
        if (interactable.CanInteract())
            await interactable.Interact(this);
        FinishedInteracting?.Invoke();
    }

    public override void _Ready()
    {
        Instance = this;
        InventoryChanged += InventoryUi.UpdateInventory;
        Puppeteer = new PlayerPuppeteer(this);
        Camera = GetNode<Camera2D>("Camera");
        //Camera.LimitBottom = GetParent<Level>().BottomEdge;
        base._Ready();
    }

    public override void _PhysicsProcess(double delta)
    {
        /*
        var y = GetViewport().GetVisibleRect().Size.Y/2;
        var tmp1 = GetParent<Level>().BottomEdge;

        Camera.Offset = new Vector2(Camera.Offset.X, (Position.Y + (tmp1 - (Position.Y + y))));

        //Camera.LimitBottom = GetParent<Level>().BottomEdge;
        var tmp = GetParent<Level>();
        */
        base._PhysicsProcess(delta);
    }

    public void ChangeSelectedItem(Item item)
    {
        SelectedItem = item;
    }
}
