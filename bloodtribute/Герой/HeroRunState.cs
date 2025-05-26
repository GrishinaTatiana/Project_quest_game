using Godot;
using System;

namespace BloodTribute
{
    public class HeroRunState : HeroState
    {
        float CollisionRelX = float.NaN;

        AudioStreamPlayer audio;

        bool isplaying = false;

        public HeroRunState(Hero Parent) : base(Parent)
        {
            audio = Parent.GetNode<Node>("Sounds").GetNode<AudioStreamPlayer>("Run");
        }

        public override void Enter()
        {
            Parent.Sprite.Animation = "Run";
            isplaying = false;
            audio.Play();
            Parent.Sprite.FrameChanged += checkShouldPlaySound;
            //audio.Finished += repeatSound;
            if(CollisionRelX is float.NaN) 
                CollisionRelX = Parent.CollisionShape.Position.X;
        }

        void repeatSound()
        {
            audio.Play();
        }

        void checkShouldPlaySound()
        {
            var t = Parent.Sprite.Frame;
            if (t == 2 || t == 7 || t == 13)
            {
                audio.Play();
            }
        }


        public override void Exit()
        {
            //audio.Finished -= repeatSound;
            Parent.Sprite.FrameChanged -= checkShouldPlaySound;
            base.Exit();
        }

        public override void PhysicsUpdate(double delta)
        {
            Parent.Sprite.FlipH = Parent.Velocity.X < 0;
            //Parent.CollisionShape.Position = new Vector2((Parent.Velocity.X < 0? -1: 1) * CollisionRelX, Parent.CollisionShape.Position.Y);
        }
    }
}
