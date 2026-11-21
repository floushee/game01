using Engine.Mathematics;
using System.Numerics;

namespace Game.Gameplay;

class Player
{
    public const int Size = 40;

    private const float Speed = 200;

    public Vector2 Position { get; private set; } = new(380, 205);

    public Vector2 Center => Position + new Vector2(Size / 2f);

    public Rectangle Bounds => new(Position.X, Position.Y, Size, Size);

    public void Update(Vector2 direction, float deltaTime, int screenWidth, int screenHeight)
    {
        // Pythagoras gives the direction's length: sqrt(x² + y²).
        // Dividing by that length makes it 1, so diagonal movement isn't faster.
        // Skip a zero-length direction to avoid division by zero.
        float length = direction.Length();
        if (length > 0) direction /= length;

        Vector2 position = Position + direction * Speed * deltaTime;
        position.X = Math.Clamp(position.X, 0, screenWidth - Size);
        position.Y = Math.Clamp(position.Y, 0, screenHeight - Size);
        Position = position;
    }

    public void Reset() => Position = new Vector2(380, 205);

}
