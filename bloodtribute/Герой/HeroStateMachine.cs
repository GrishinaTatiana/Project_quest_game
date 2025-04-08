using BloodTribute;
using Godot;
using System.Collections.Generic;
using System.Threading;

public partial class HeroStateMachine : Node
{
    List <HeroState> states;

    HeroState currentState;

    public bool IsBlockedInputs = false;

    bool allowDefaulting = true;

    Hero Parent;

    public override void _Ready()
    {
        Parent = GetParent<Hero>();
        states = new();

        HeroState tmp = new HeroIdleState(Parent);
        states.Add(tmp);

        tmp = new HeroRunState(Parent);
        tmp.Exited += ResetStates;
        states.Add(tmp);

        tmp = new HeroInteractState(this, Parent);
        tmp.Exited += ResetStates;
        states.Add(tmp);

        states.Sort((x, y) => x.StateType.CompareTo(y.StateType));
        currentState = states[states.Count - 1];
    }

    void ResetStates()
    {
        allowDefaulting = true;
        currentState = null;
    }

    void ChangeState(HeroState state)
    {
        if(currentState == state) return;
        GD.Print("Entered " + state.StateType.ToString() + " state");
        if(currentState != null)
            currentState.Exit();
        currentState = state;
        currentState.Enter();
        allowDefaulting = false;
    }

    public override void _Process(double delta)
    {
        if (!IsBlockedInputs)
        {
            var i = 0;
            for (; i< states.Count - 1; i++)
            {
                if (states[i].ShouldActivate())
                {
                    ChangeState(states[i]);
                    return;
                }
            }
            if(allowDefaulting)
                ChangeState(states[i]);
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        if(currentState != null) currentState.PhysicsUpdate(delta);
        base._PhysicsProcess(delta);
    }
}

public enum HeroStates
{
    Idle = 2,
    Run = 1,
    Interact = 0
}


public abstract class HeroState : State
{
    public abstract HeroStates StateType { get; }

    protected HeroState(Character Parent) : base(Parent)
    {
    }

    public abstract bool ShouldActivate();
}
