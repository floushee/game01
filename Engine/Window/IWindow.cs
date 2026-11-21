namespace Engine.Window;

public interface IWindow
{
    void InitWindow(int width, int height, string title);

    void CloseWindow();

    void SetTargetFPS(int fps);

    void HideCursor();

    bool WindowShouldClose();

    float GetFrameTime();
}
