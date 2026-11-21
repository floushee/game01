using Engine;
using Engine.Audio;
using Engine.Rendering;

namespace Game.Resources;

sealed class GameResources : IDisposable
{
    private readonly IEngine engine;
    private readonly Stack<ITexture> textures = new();

    private bool disposed;

    public ITexture Crosshair { get; }

    public PlayerSpriteSet PlayerSprites { get; }

    public EnemySpriteSet EnemySprites { get; }

    public IMusic Music { get; private set; } = null!;

    public ISound DeathSound { get; private set; } = null!;

    public ISound LaserSound { get; private set; } = null!;

    public GameResources(IEngine engine)
    {
        this.engine = engine;
        try
        {
            Crosshair = LoadTexture("Crosshair.png");
            PlayerSprites = new PlayerSpriteSet(LoadTexture("Player.png"),
                LoadTexture("PlayerWalkA.png"), LoadTexture("PlayerWalkB.png"),
                LoadTexture("PlayerRise.png"), LoadTexture("PlayerFall.png"));
            EnemySprites = LoadEnemySprites();
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

    private EnemySpriteSet LoadEnemySprites()
    {
        ITexture neutral = LoadTexture("Enemy.png");
        ITexture thrust = LoadTexture("EnemyFlightA.png");
        ITexture boost = LoadTexture("EnemyFlightB.png");
        return new EnemySpriteSet([neutral, thrust, boost, thrust]);
    }

    private ITexture LoadTexture(string file)
    {
        ITexture texture = engine.Renderer.LoadTexture(AssetPath("Images", file));
        textures.Push(texture);
        return texture;
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
        while (textures.TryPop(out ITexture? texture)) engine.Renderer.UnloadTexture(texture);
    }
}
