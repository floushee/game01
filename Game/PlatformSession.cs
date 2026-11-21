using Engine;

namespace Game;

sealed class Platform : IDisposable
{
    private readonly IEngine engine;

    private bool windowOpen;

    private bool audioOpen;

    public Platform(IEngine engine, int width, int height, int targetFps, string title)
    {
        this.engine = engine;
        try
        {
            engine.Window.InitWindow(width, height, title);
            windowOpen = true;
            engine.Window.SetTargetFPS(targetFps);
            engine.Window.HideCursor();
            engine.Audio.InitAudioDevice();
            audioOpen = true;
        }
        catch
        {
            Dispose();
            throw;
        }
    }

    public void Dispose()
    {
        try
        {
            if (audioOpen)
            {
                audioOpen = false;
                engine.Audio.CloseAudioDevice();
            }
        }
        finally
        {
            if (windowOpen)
            {
                windowOpen = false;
                engine.Window.CloseWindow();
            }
        }
    }
}
