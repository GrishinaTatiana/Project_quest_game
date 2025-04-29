using Godot;
using System;

namespace BloodTribute
{
    public class HeroRunState : HeroState
    {
        public HeroRunState(Hero Parent) : base(Parent)
        {
        }

        public override void Enter()
        {
            Parent.Sprite.Animation = "Run"; 
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void PhysicsUpdate(double delta)
        {
            Parent.Sprite.FlipH = Parent.Velocity.X < 0;
        }
    }
}
