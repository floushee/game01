using Engine;
using Engine.Audio;
using Engine.Rendering;

namespace Game.Resources;

sealed class GameResources : IDisposable
{
    private readonly IEngine engine;

    private bool disposed;

    public ITexture Crosshair { get; private set; } = null!;

    public IMusic Music { get; private set; } = null!;

    public ISound DeathSound { get; private set; } = null!;

    public ISound LaserSound { get; private set; } = null!;

    public GameResources(IEngine engine)
    {
        this.engine = engine;
        try
        {
            Crosshair = engine.Renderer.LoadTexture(AssetPath("Images", "Crosshair.png"));
            Music = engine.Audio.LoadMusicStream(AssetPath("Audio", "Music.wav"));
            DeathSound = engine.Audio.LoadSound(AssetPath("Audio", "Death.wav"));
            LaserSound = engine.Audio.LoadSound(AssetPath("Audio", "Laser.wav"));
            engine.Audio.PlayMusicStream(Music);
        }
        catch
        {
            Dispose();
            throw;
        }
    }

    private static string AssetPath(string folder, string file) =>
        Path.Combine(AppContext.BaseDirectory, "Assets", folder, file);

    public void Dispose()
    {
        if (disposed) return;
        disposed = true;
        if (LaserSound is not null) engine.Audio.UnloadSound(LaserSound);
        if (DeathSound is not null) engine.Audio.UnloadSound(DeathSound);
        if (Music is not null) engine.Audio.UnloadMusicStream(Music);
        if (Crosshair is not null) engine.Renderer.UnloadTexture(Crosshair);
    }
}
