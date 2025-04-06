using Godot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodTribute
{
    public class HeroIdleState : HeroState
    {
        public override HeroStates State => HeroStates.Idle;
        public HeroIdleState(Hero Parent) : base(Parent)
        {
        }

        public override void Enter()
        {
            var flipped = Parent.Sprite.FlipH;
            GD.Print("Entered Idle state");
            Parent.Sprite.Animation = "Idle";
            Parent.Sprite.FlipH = flipped;
        }

        public override void Exit()
        {
            GD.Print("Exited Idle state");
            base.Exit();
        }

        public override void PhysicsUpdate(double delta)
        {
            Parent.Velocity = new Vector2(0, Parent.Gravity);
            Parent.MoveAndSlide();
        }

        public override void Update(double delta)
        {
            
        }
    }
}
