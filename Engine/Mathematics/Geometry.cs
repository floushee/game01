using System.Numerics;

namespace Engine.Mathematics;

public static class Geometry
{
    public static bool CheckCollisionRecs(Rectangle a, Rectangle b) =>
        a.X < b.X + b.Width && a.X + a.Width > b.X && a.Y < b.Y + b.Height && a.Y + a.Height > b.Y;

    public static bool CheckCollisionCircleRec(Vector2 center, float radius, Rectangle rect)
    {
        float x = Math.Clamp(center.X, rect.X, rect.X + rect.Width);
        float y = Math.Clamp(center.Y, rect.Y, rect.Y + rect.Height);
        return Vector2.DistanceSquared(center, new Vector2(x, y)) <= radius * radius;
    }
}
