using BloodTribute;
using Godot;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

internal class HeroInteractState : HeroState
{
    public HeroInteractState(Hero Parent) : base(Parent)
    {
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Enter()
    {
        var tmp = Parent.Sprite.FlipH;
        Parent.Sprite.Animation = "Interact";
        Parent.Sprite.FlipH = tmp;
    }

    public override void PhysicsUpdate(double delta)
    {
    }
}
