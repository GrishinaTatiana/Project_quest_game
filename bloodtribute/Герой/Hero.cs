using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Hero : Character
{
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
}
