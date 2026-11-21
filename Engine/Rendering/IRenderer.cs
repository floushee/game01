using System.Numerics;

namespace Engine.Rendering;

public interface IRenderer
{
    ITexture LoadTexture(string path);

    void UnloadTexture(ITexture texture);

    void BeginDrawing();

    void EndDrawing();

    void ClearBackground(Color color);

    void DrawRectangle(int x, int y, int width, int height, Color color);

    void DrawCircleV(Vector2 position, float radius, Color color);

    void DrawText(string text, int x, int y, int size, Color color);

    void DrawTexture(ITexture texture, int x, int y, Color color);
}
