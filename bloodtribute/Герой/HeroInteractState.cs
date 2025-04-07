using BloodTribute;
using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

internal class HeroInteractState : HeroState
{
    HeroStateMachine machine;
    IInteractable CurrentInteraction;
    List<IInteractable> CurrentInteractables;

    public override HeroStates StateType => HeroStates.Interact;

    public HeroInteractState(HeroStateMachine machine, Hero Parent) : base(Parent)
    {
        this.machine = machine;
    }


    public override void Exit()
    {
        Parent.Sprite.AnimationLooped -= Exit;
        base.Exit();
    }

    public override void Enter()
    {
        Parent.Sprite.Animation = "Interact";
        StartInteraction(CurrentInteractables[0]);
    }

    public void StartInteraction(IInteractable obj)
    {
        machine.IsBlockedInputs = true;
        CurrentInteraction = obj;
        obj.InteractionFinished += ReturnControl;
        obj.Interact(Parent);
    }

    void ReturnControl()
    {
        machine.IsBlockedInputs = false;
        CurrentInteraction.InteractionFinished -= ReturnControl;
        CurrentInteraction = null;
        Parent.Sprite.AnimationLooped += Exit;
    }

    public override void PhysicsUpdate(double delta)
    {
        Parent.Velocity = new Vector2(0, Parent.Gravity);
        Parent.MoveAndSlide();
    }

    public override void Update(double delta)
    {
        throw new System.NotImplementedException();
    }

    public override bool ShouldActivate()
    {
        if (Input.IsActionJustPressed("InteractMouse"))
        {
            CurrentInteractables = ((Hero)Parent).GetIInteractableAreasUnderCursor();
            return CurrentInteractables.Count > 0;
        }
        else if (Input.IsActionJustPressed("InteractKey"))
        {
            CurrentInteractables = ((Hero)Parent).GetIInteractableOverlappingAreas();
            return CurrentInteractables.Count > 0;
        }
        return false;
    }
}
