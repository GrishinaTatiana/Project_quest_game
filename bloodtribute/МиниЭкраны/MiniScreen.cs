using Godot;
using System;

public partial class MiniScreen : CanvasLayer
{
    [Export]
    public Sprite2D Background { get; set; }

    [Export]
    TextureButton ExitButton { get; set; }


    [Signal]
    public delegate void ExitEventHandler();

    private Vector2 _baseResolution;

    public override void _Ready()
    {
        ExitButton.Pressed += () => EmitSignal(SignalName.Exit);



        //_baseResolution = Background.GetRect().Size;
        //GetViewport().SizeChanged += UpdateScale;
    }

    private void UpdateScale()
    {
        Vector2 currentSize = GetViewport().GetVisibleRect().Size;

        Vector2 scale = new Vector2(
            currentSize.X / _baseResolution.X,
            currentSize.Y / _baseResolution.Y
        );

        // Применяем масштаб ко всем дочерним Control-нодам
        foreach (Node child in GetChildren())
        {
            if (child is Node2D node)
            {
                node.Scale = scale;
            }
        }
        _baseResolution = currentSize;
    }
}
