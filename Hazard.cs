using System.Numerics;
using Raylib_cs;

class Hazard
{
    private const int Size = 30;
    private const float InitialSpeed = 100;
    private const float Acceleration = 5;
    private const float MaxSpeed = 240;

    public Vector2 Position { get; private set; } = new(0, 100);

    public Rectangle Bounds => new(Position.X, Position.Y, Size, Size);
    
    public void Update(float deltaTime, float survivalTime, Vector2 playerCenter)
    {
        Vector2 chase = playerCenter - (Position + new Vector2(Size / 2f));
        float distance = chase.Length();
        float speed = MathF.Min(InitialSpeed + Acceleration * survivalTime, MaxSpeed);
        if (distance > 0) Position += chase / distance * speed * deltaTime;
    }

    public void Reset() => Position = new Vector2(0, 100);

    public void Draw() => Raylib.DrawRectangle((int)Position.X, (int)Position.Y, Size, Size, Color.Red);
}
