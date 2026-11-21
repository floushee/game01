using Engine.Audio;
using Engine.Input;
using Engine.Rendering;
using Engine.Mathematics;
using System.Numerics;
using Engine;

namespace Game;

public sealed class Game : IDisposable
{
    private const int ScreenWidth = 800;

    private const int ScreenHeight = 450;

    private const float ProjectileSpeed = 500;

    private readonly Player player = new();

    private readonly Hazard hazard = new();

    private readonly List<Projectile> projectiles = [];

    private readonly ITexture target;

    private readonly GameRenderer renderer;

    private readonly IMusic music;

    private readonly ISound deathSound;

    private readonly ISound laserSound;

    private bool gameOver;

    private float survivalTime;

    private int score;

    private int bestScore;

    private readonly IEngine engine;

    public Game(IEngine engine)
    {
        this.engine = engine;
        renderer = new GameRenderer(engine);
        engine.InitWindow(ScreenWidth, ScreenHeight, "Game 01");
        engine.SetTargetFPS(60);
        target = engine.LoadTexture(AssetPath("Images", "Target.png"));
        engine.HideCursor();

        engine.InitAudioDevice();
        music = engine.LoadMusicStream(AssetPath("Audio", "Music.wav"));
        deathSound = engine.LoadSound(AssetPath("Audio", "Death.wav"));
        laserSound = engine.LoadSound(AssetPath("Audio", "Laser.wav"));
        engine.PlayMusicStream(music);
    }

    public void Run()
    {
        while (!engine.WindowShouldClose())
        {
            float deltaTime = engine.GetFrameTime();
            engine.UpdateMusicStream(music);
            Update(deltaTime);
            renderer.Draw(player, hazard, projectiles, score, bestScore, gameOver, target, engine.GetMousePosition());
        }
    }

    private void Update(float deltaTime)
    {
        if (gameOver)
        {
            if (engine.IsKeyPressed(Key.R)) Reset();
            return;
        }

        survivalTime += deltaTime;
        player.Update(engine, deltaTime, ScreenWidth, ScreenHeight);
        hazard.Update(deltaTime, survivalTime, player.Center);

        if (engine.IsMouseButtonPressed(MouseButton.Left))
        {
            Vector2 aim = engine.GetMousePosition() - player.Center;
            if (aim.LengthSquared() > 0)
            {
                projectiles.Add(new Projectile(player.Center, Vector2.Normalize(aim) * ProjectileSpeed));
                engine.PlaySound(laserSound);
            }
        }

        // Iterate backwards so removing a projectile doesn't skip the next one.
        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            Projectile projectile = projectiles[i];
            projectile.Update(deltaTime);
            if (Geometry.CheckCollisionCircleRec(projectile.Position, Projectile.Radius, hazard.Bounds))
            {
                score++;
                projectiles.RemoveAt(i);
            }
            else if (projectile.IsOutside(ScreenWidth, ScreenHeight))
            {
                projectiles.RemoveAt(i);
            }
            else
            {
                projectiles[i] = projectile;
            }
        }

        if (Geometry.CheckCollisionRecs(player.Bounds, hazard.Bounds))
        {
            gameOver = true;
            engine.PlaySound(deathSound);
            bestScore = Math.Max(bestScore, score);
        }
    }

    private void Reset()
    {
        player.Reset();
        hazard.Reset();
        survivalTime = 0;
        score = 0;
        projectiles.Clear();
        gameOver = false;
    }

    private static string AssetPath(string folder, string file) =>
        Path.Combine(AppContext.BaseDirectory, "Assets", folder, file);

    public void Dispose()
    {
        engine.UnloadSound(laserSound);
        engine.UnloadSound(deathSound);
        engine.UnloadMusicStream(music);
        engine.CloseAudioDevice();
        engine.UnloadTexture(target);
        engine.CloseWindow();
    }
}
