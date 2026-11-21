using Engine.Window;
using Raylib_cs;

namespace RaylibEngine.Window;

internal sealed class RaylibWindow : IWindow
{
    public void InitWindow(int width, int height, string title) => Raylib.InitWindow(width, height, title);

    public void CloseWindow() => Raylib.CloseWindow();

    public void SetTargetFPS(int fps) => Raylib.SetTargetFPS(fps);

    public void HideCursor() => Raylib.HideCursor();

    public bool WindowShouldClose() => Raylib.WindowShouldClose();

    public float GetFrameTime() => Raylib.GetFrameTime();
}
