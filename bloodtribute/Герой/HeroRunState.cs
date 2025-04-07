using Godot;
using System;

namespace BloodTribute
{
    public class HeroRunState : HeroState
    {
        public override HeroStates StateType => HeroStates.Run;

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
            var tmp = Input.GetAxis("MoveLeft", "MoveRight");

            if (tmp == 0)
            {
                Exit();
                return;
            }

            Parent.Sprite.FlipH = tmp < 0;
            Parent.Velocity = new Vector2(tmp * Parent.Speed, Parent.Gravity);
            Parent.MoveAndSlide();
        }

        public override void Update(double delta)
        {
            throw new NotImplementedException();
        }

        public override bool ShouldActivate()
        {
            return Input.GetAxis("MoveLeft", "MoveRight") != 0;
        }
    }
}
