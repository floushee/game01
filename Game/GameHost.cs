using Game.Gameplay;
using Game.Input;
using Game.Rendering;
using Game.Resources;
using Engine;

namespace Game;

public sealed class GameHost : IDisposable
{
    private const int ScreenWidth = 800;

    private const int ScreenHeight = 450;

    private const int TargetFps = 60;

    private const string GameTitle = "Game 01";

    private readonly GameState state;

    private readonly IEngine engine;

    private readonly GameRenderer renderer;

    private readonly GameInputMapper input;

    private readonly GameResources resources;

    private readonly Platform platform;

    public GameHost(IEngine engine)
    {
        this.engine = engine;
        state = new GameState(ScreenWidth, ScreenHeight);
        renderer = new GameRenderer(engine.Renderer);
        input = new GameInputMapper(engine.Input);
        platform = new Platform(engine, ScreenWidth, ScreenHeight, TargetFps, GameTitle);
        try
        {
            resources = new GameResources(engine);
        }
        catch
        {
            platform.Dispose();
            throw;
        }
    }

    public void Run()
    {
        while (!engine.Window.WindowShouldClose())
        {
            float deltaTime = engine.Window.GetFrameTime();
            engine.Audio.UpdateMusicStream(resources.Music);
            GameInput frameInput = input.Read();
            GameEvents events = state.Update(deltaTime, frameInput);
            if ((events & GameEvents.Shot) != 0) engine.Audio.PlaySound(resources.LaserSound);
            if ((events & GameEvents.Death) != 0) engine.Audio.PlaySound(resources.DeathSound);
            renderer.Draw(state, resources.Crosshair, frameInput.PlayerAim);
        }
    }

    public void Dispose()
    {
        try
        {
            resources.Dispose();
        }
        finally
        {
            platform.Dispose();
        }
    }
}
