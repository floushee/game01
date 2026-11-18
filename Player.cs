using System.Numerics;
using Raylib_cs;

class Player
{
    public const int Size = 40;
    private const float Speed = 200;
    
    public Vector2 Position { get; private set; } = new(380, 205);

    public Vector2 Center => Position + new Vector2(Size / 2f);

    public Rectangle Bounds => new(Position.X, Position.Y, Size, Size);

    public void Update(float deltaTime, int screenWidth, int screenHeight)
    {
        Vector2 direction = Vector2.Zero;
        if (Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.Right)) direction.X++;
        if (Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.Left)) direction.X--;
        if (Raylib.IsKeyDown(KeyboardKey.S) || Raylib.IsKeyDown(KeyboardKey.Down)) direction.Y++;
        if (Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown(KeyboardKey.Up)) direction.Y--;

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

    public void Draw() => Raylib.DrawRectangle((int)Position.X, (int)Position.Y, Size, Size, Color.Green);
}
