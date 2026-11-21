using System.Numerics;
using Engine.Audio;
using Engine.Input;
using Engine.Rendering;

namespace Engine;

public interface IEngine : IInput, IRenderer, IAudio
{
    void InitWindow(int width, int height, string title);

    void CloseWindow();

    void SetTargetFPS(int fps);

    void HideCursor();

    bool WindowShouldClose();

    float GetFrameTime();
}
