using Godot;
using System;

namespace BloodTribute
{
    public class HeroRunState : HeroState
    {
        public override HeroStates State => HeroStates.Run;

        public HeroRunState(Hero Parent) : base(Parent)
        {
        }

        public override void Enter()
        {
            GD.Print("Entered Run state");
            Parent.Sprite.Animation = "Run"; 
        }

        public override void Exit()
        {
            GD.Print("Exited Run state");
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
    }
}
