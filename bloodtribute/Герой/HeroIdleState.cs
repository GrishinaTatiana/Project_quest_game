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
        public HeroIdleState(Hero Parent) : base(Parent)
        {
        }

        public override void Enter()
        {
            var flipped = Parent.Sprite.FlipH;
            Parent.Sprite.Animation = "Idle";
            Parent.Sprite.FlipH = flipped;
        }

        public override void Exit()
        {
            base.Exit();
        }

        public override void PhysicsUpdate(double delta)
        {
        }
    }
}
