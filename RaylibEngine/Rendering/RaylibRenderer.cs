using System.Numerics;
using Engine.Rendering;
using Raylib_cs;
using Color = Engine.Rendering.Color;

namespace RaylibEngine.Rendering;

internal sealed class RaylibRenderer : IRenderer
{
    private static Raylib_cs.Color Convert(Color c) => new(c.R, c.G, c.B, c.A);

    public ITexture LoadTexture(string path) => new Texture(Raylib.LoadTexture(path));

    public void UnloadTexture(ITexture texture) => Raylib.UnloadTexture(((Texture)texture).Value);

    public void BeginDrawing() => Raylib.BeginDrawing();

    public void EndDrawing() => Raylib.EndDrawing();

    public void ClearBackground(Color color) => Raylib.ClearBackground(Convert(color));

    public void DrawRectangle(int x, int y, int width, int height, Color color) => Raylib.DrawRectangle(x, y, width, height, Convert(color));

    public void DrawCircleV(Vector2 position, float radius, Color color) => Raylib.DrawCircleV(position, radius, Convert(color));

    public void DrawText(string text, int x, int y, int size, Color color) => Raylib.DrawText(text, x, y, size, Convert(color));

    public void DrawTexture(ITexture texture, int x, int y, Color color, bool flipHorizontally = false)
    {
        // Raylib mirrors the source region when its width is negative.
        var source = new Rectangle(0, 0, flipHorizontally ? -texture.Width : texture.Width, texture.Height);
        Raylib.DrawTextureRec(((Texture)texture).Value, source, new Vector2(x, y), Convert(color));
    }
}
