using Godot;
using System;

namespace BloodTribute
{
    public class HeroRunState : HeroState
    {
        float CollisionRelX = float.NaN;


        public HeroRunState(Hero Parent) : base(Parent)
        {
        }

        public override void Enter()
        {
            Parent.Sprite.Animation = "Run";
            if(CollisionRelX is float.NaN) 
                CollisionRelX = Parent.CollisionShape.Position.X;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void PhysicsUpdate(double delta)
        {
            Parent.Sprite.FlipH = Parent.Velocity.X < 0;
            //Parent.CollisionShape.Position = new Vector2((Parent.Velocity.X < 0? -1: 1) * CollisionRelX, Parent.CollisionShape.Position.Y);
        }
    }
}
