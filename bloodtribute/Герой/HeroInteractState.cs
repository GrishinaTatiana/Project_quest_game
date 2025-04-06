using BloodTribute;
using Godot;
using System;
using System.Threading.Tasks;

internal class HeroInteractState : HeroState
{
    HeroStateMachine machine;
    IInteractable CurrentInteraction;

    public HeroInteractState(HeroStateMachine machine, Hero Parent) : base(Parent)
    {
        this.machine = machine;
    }


    public override void Exit()
    {
        Parent.Sprite.AnimationLooped -= Exit;
        base.Exit();
    }

    public override HeroStates State => HeroStates.Interact;

    public override void Enter()
    {
        Parent.Sprite.Animation = "Interact";
    }

    public async Task StartInteraction(IInteractable obj)
    {
        machine.IsBlockedInputs = true;
        CurrentInteraction = obj;
        obj.InteractionFinished += ReturnControl;
        await obj.Interact(Parent);
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
}
