using System;
using Godot;
using BloodTribute;

public abstract class State
{
    protected Character Parent;

    public event Action Exited;

    public State(Character Parent)
    {
        this.Parent = Parent;
    }

    public abstract void Enter();

    public virtual void Exit()
    {
        Exited?.Invoke();
    }

    public abstract void Update(double delta);

    public abstract void PhysicsUpdate(double delta);
}
