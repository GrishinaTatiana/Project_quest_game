using BloodTribute;
using Godot;
using System.Collections.Generic;
using System.Threading;

public partial class HeroStateMachine : Node
{
    Dictionary<HeroStates, HeroState> states;

    HeroState currentState;
    HeroState defaultState;

    public bool IsBlockedInputs = false;

    Hero Parent;

    public override void _Ready()
    {
        Parent = GetParent<Hero>();
        states = new();
        
        states[HeroStates.Idle] = new HeroIdleState(Parent);

        states[HeroStates.Run] = new HeroRunState(Parent);
        states[HeroStates.Run].Exited += SwitchToDefault;

        states[HeroStates.Interact] = new HeroInteractState(this, Parent);
        states[HeroStates.Interact].Exited += SwitchToDefault;

        currentState = states[HeroStates.Idle];
        defaultState = states[HeroStates.Idle];

        Parent.Ready += () => currentState.Enter();
    }

    void SwitchToDefault()
    {
        currentState = defaultState;
        currentState.Enter();
    }

    void ChangeState(HeroStates state)
    {
        currentState.Exit();
        currentState = states[state];
        currentState.Enter();
    }

    public async override void _Process(double delta)
    {
        if(IsBlockedInputs)
            return;

        if(Input.IsActionJustPressed("InteractMouse"))
        {
            GD.Print("Clicked");
            var tmp = GetAreasUnderCursor();
            if (tmp.Count > 0)
            {
                ChangeState(HeroStates.Interact);
                await ((HeroInteractState)currentState).StartInteraction(tmp[0]);
            }
            return;
        }
        if (Input.IsActionJustPressed("InteractKey"))
        {
            GD.Print("PressedE");
            foreach(var area in Parent.ReachArea.GetOverlappingAreas())
                if(area.GetParent() is IInteractable tmp)
                {
                    ChangeState(HeroStates.Interact);
                    await ((HeroInteractState)currentState).StartInteraction(tmp);
                    break;
                }
        }
        if(currentState.State != HeroStates.Run && Input.GetAxis("MoveLeft", "MoveRight") != 0)
        {
            ChangeState(HeroStates.Run);
            return;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        currentState.PhysicsUpdate(delta);
        base._PhysicsProcess(delta);
    }

    List<IInteractable> GetAreasUnderCursor()
    {
        var list = new List<IInteractable>();
        var spaceState = Parent.GetWorld2D().DirectSpaceState;
        var query = new PhysicsPointQueryParameters2D();
        query.Position = Parent.GetGlobalMousePosition();
        query.CollisionMask = 2;
        query.CollideWithAreas = true;
        var result = spaceState.IntersectPoint(query);
        foreach (var area in result)
        {
            var a = (Area2D)area["collider"];
            if (Parent.ReachArea.GetOverlappingAreas().Contains(a))
                if (a.GetParent() is IInteractable tmp)
                    list.Add(tmp);
        }
        return list;
    }
}

public abstract class HeroState : State
{
    public abstract HeroStates State { get; }
    protected HeroState(Character Parent) : base(Parent)
    {
    }
}

public enum HeroStates
{
    Idle,
    Run,
    Interact
}
