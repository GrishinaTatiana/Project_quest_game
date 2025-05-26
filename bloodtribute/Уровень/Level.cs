using Godot;
using System;
using BloodTribute;

public partial class Level : Node2D
{
    public int BottomEdge;
    public int TopEdge;
    public int RightEdge;
    public int LeftEdge;

    public AudioStreamPlayer Audio;

    public Hero hero;

    public override void _Ready()
    {
        var t = GetNode<Sprite2D>("Sprite2D");
        Audio = GetNode<AudioStreamPlayer>("Audio");

        BottomEdge = (int)(t.GlobalPosition.Y + t.GetRect().Size.Y/2);
        RightEdge = (int)(t.GlobalPosition.X + t.GetRect().Size.X /2);
        LeftEdge = (int)(t.GlobalPosition.X - t.GetRect().Size.X/2);
        TopEdge = (int)(t.GlobalPosition.Y - t.GetRect().Size.Y/2);

        var tmp = t.GenerateBoundaries();
        t.AddChild(tmp);

        base._Ready();
    }

    public void Enter(Hero hero)
    {
        this.hero = hero;

        hero.Camera.LimitBottom = BottomEdge;
        hero.Camera.LimitLeft = LeftEdge;
        hero.Camera.LimitRight = RightEdge;
        AddChild(this.hero);
        Audio.Play();
    }

    public Hero Exit()
    {
        var tmp = this.hero;
        this.hero = null;
        RemoveChild(tmp);
        Audio.Stop();
        return tmp;
    }

}
