using Godot;
using BloodTribute;

public static class Sprite2DExtensions
{
    public static StaticBody2D GenerateBoundaries(this Sprite2D sprite)
    {
        var body = new StaticBody2D();
        var rect = sprite.GetRect();
        var size = rect.Size;

        var points = new Vector2[] { rect.Position, new Vector2(rect.Position.X, rect.End.Y), rect.End, new Vector2(rect.End.X, rect.Position.Y), rect.Position };
        
        for (int i = 0;  i < points.Length - 1; i++)
        {
            var shape = new SegmentShape2D();
            shape.A = points[i];
            shape.B = points[i + 1];
            var leftBoundary = new CollisionShape2D();
            leftBoundary.Shape = shape;
            body.AddChild(leftBoundary);
        }

        return body;
    }

    public static CollisionShape2D GenerateShape(this Sprite2D sprite)
    {
        var shape = new CollisionShape2D();
        var tmp = new RectangleShape2D();
        tmp.Size = sprite.GetRect().Size * sprite.Scale;
        shape.Shape = tmp;

        return shape;
    }
}
