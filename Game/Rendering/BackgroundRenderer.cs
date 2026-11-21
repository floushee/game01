using Engine.Rendering;

namespace Game.Rendering;

sealed class LevelRenderer(IRenderer renderer)
{
    private const int StarCount = 85;

    private static readonly (int X, int Y, int Size, Color Tint)[] Stars = CreateStars();

    public void Draw()
    {
        renderer.ClearBackground(Color.Black);
        DrawStars();
        DrawGround();
    }

    private static (int X, int Y, int Size, Color Tint)[] CreateStars()
    {
        var random = new Random(42);
        var stars = new (int X, int Y, int Size, Color Tint)[StarCount];
        for (int i = 0; i < stars.Length; i++)
        {
            int size = random.Next(5) == 0 ? 2 : 1;
            byte brightness = (byte)random.Next(85, size == 2 ? 235 : 170);
            stars[i] = (random.Next(Constants.ScreenWidth - size + 1),
                random.Next(Constants.GroundY - size + 1), size,
                new Color(brightness, brightness, (byte)(brightness + 20)));
        }
        return stars;
    }

    private void DrawStars()
    {
        foreach (var (x, y, size, tint) in Stars)
            renderer.DrawRectangle(x, y, size, size, tint);
    }

    private void DrawGround()
    {
        renderer.DrawRectangle(0, Constants.GroundY, Constants.ScreenWidth, Constants.GroundHeight, new Color(27, 87, 44));
        renderer.DrawRectangle(0, Constants.GroundY, Constants.ScreenWidth, 3, new Color(87, 170, 74));
    }
}
