using System.Numerics;
using Raylib_cs;

sealed class Game : IDisposable
{
    private const int ScreenWidth = 800;
    private const int ScreenHeight = 450;
    private const float ProjectileSpeed = 500;

    private readonly Player player = new();
    private readonly Hazard hazard = new();
    private readonly List<Projectile> projectiles = [];
    private readonly Texture2D target;
    private readonly Music music;
    private readonly Sound deathSound;
    private readonly Sound laserSound;
    private bool gameOver;
    private float survivalTime;
    private int score;
    private int bestScore;

    public Game()
    {
        Raylib.InitWindow(ScreenWidth, ScreenHeight, "Stay Alive");
        Raylib.SetTargetFPS(60);
        target = Raylib.LoadTexture(AssetPath("Images", "Target.png"));
        Raylib.HideCursor();

        Raylib.InitAudioDevice();
        music = Raylib.LoadMusicStream(AssetPath("Audio", "Music.wav"));
        deathSound = Raylib.LoadSound(AssetPath("Audio", "Death.wav"));
        laserSound = Raylib.LoadSound(AssetPath("Audio", "Laser.wav"));
        Raylib.PlayMusicStream(music);
    }

    public void Run()
    {
        while (!Raylib.WindowShouldClose())
        {
            float deltaTime = Raylib.GetFrameTime();
            Raylib.UpdateMusicStream(music);
            Update(deltaTime);
            Draw();
        }
    }

    private void Update(float deltaTime)
    {
        if (gameOver)
        {
            if (Raylib.IsKeyPressed(KeyboardKey.R)) Reset();
            return;
        }

        survivalTime += deltaTime;
        player.Update(deltaTime, ScreenWidth, ScreenHeight);
        hazard.Update(deltaTime, survivalTime, player.Center);

        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Vector2 aim = Raylib.GetMousePosition() - player.Center;
            if (aim.LengthSquared() > 0)
            {
                projectiles.Add(new Projectile(player.Center, Vector2.Normalize(aim) * ProjectileSpeed));
                Raylib.PlaySound(laserSound);
            }
        }

        // Iterate backwards so removing a projectile doesn't skip the next one.
        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            Projectile projectile = projectiles[i];
            projectile.Update(deltaTime);
            if (Raylib.CheckCollisionCircleRec(projectile.Position, Projectile.Radius, hazard.Bounds))
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

        if (Raylib.CheckCollisionRecs(player.Bounds, hazard.Bounds))
        {
            gameOver = true;
            Raylib.PlaySound(deathSound);
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

    private void Draw()
    {
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);
        player.Draw();
        hazard.Draw();
        foreach (Projectile projectile in projectiles) projectile.Draw();
        Raylib.DrawText($"Hits: {score}", 20, 20, 24, Color.White);
        Raylib.DrawText($"Best: {bestScore}", 20, 50, 24, Color.White);
        if (gameOver) Raylib.DrawText("Game over! Press R to restart", 200, 20, 24, Color.White);
        Vector2 mouse = Raylib.GetMousePosition();
        Raylib.DrawTexture(target, (int)(mouse.X - target.Width / 2f), (int)(mouse.Y - target.Height / 2f), Color.White);
        Raylib.EndDrawing();
    }

    private static string AssetPath(string folder, string file) =>
        Path.Combine(AppContext.BaseDirectory, "Assets", folder, file);

    public void Dispose()
    {
        Raylib.UnloadSound(laserSound);
        Raylib.UnloadSound(deathSound);
        Raylib.UnloadMusicStream(music);
        Raylib.CloseAudioDevice();
        Raylib.UnloadTexture(target);
        Raylib.CloseWindow();
    }
}
