using System.Numerics;
using Raylib_cs;

const int screenWidth = 800;
const int screenHeight = 450;

Raylib.InitWindow(screenWidth, screenHeight, "Stay Alive");
Raylib.SetTargetFPS(60);
string targetPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Images", "Target.png");
Texture2D target = Raylib.LoadTexture(targetPath);
Raylib.HideCursor();

Raylib.InitAudioDevice();
string musicPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Audio", "Music.wav");
Music music = Raylib.LoadMusicStream(musicPath);
string deathSoundPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Audio", "Death.wav");
Sound deathSound = Raylib.LoadSound(deathSoundPath);
string laserSoundPath = Path.Combine(AppContext.BaseDirectory, "Assets", "Audio", "Laser.wav");
Sound laserSound = Raylib.LoadSound(laserSoundPath);
Raylib.PlayMusicStream(music);

float playerX = 380;
float playerY = 205;
const float playerSpeed = 200; // Pixels per second.
const int playerSize = 40;

float hazardX = 0;
float hazardY = 100;
const int hazardSize = 30;
const float initialHazardSpeed = 100; // Pixels per second.
const float hazardAcceleration = 5; // Speed gained per second.
const float maxHazardSpeed = 240;
bool gameOver = false;
float survivalTime = 0; // Used to increase difficulty.
int score = 0;
int bestScore = 0; // Best run since the game was opened.
List<Projectile> projectiles = [];
const float projectileSpeed = 500; // Pixels per second.
const float projectileRadius = 5;

while (!Raylib.WindowShouldClose())
{
    float deltaTime = Raylib.GetFrameTime();
    Raylib.UpdateMusicStream(music);

    if (!gameOver)
    {
        survivalTime += deltaTime;

        float directionX = 0;
        float directionY = 0;
        if (Raylib.IsKeyDown(KeyboardKey.D) || Raylib.IsKeyDown(KeyboardKey.Right)) directionX += 1;
        if (Raylib.IsKeyDown(KeyboardKey.A) || Raylib.IsKeyDown(KeyboardKey.Left)) directionX -= 1;
        if (Raylib.IsKeyDown(KeyboardKey.S) || Raylib.IsKeyDown(KeyboardKey.Down)) directionY += 1;
        if (Raylib.IsKeyDown(KeyboardKey.W) || Raylib.IsKeyDown(KeyboardKey.Up)) directionY -= 1;

        // Pythagoras gives the direction's length: sqrt(x² + y²).
        // Dividing by that length makes it 1, so diagonal movement isn't faster.
        // Skip a zero-length direction to avoid division by zero.
        float directionLength = MathF.Sqrt(directionX * directionX + directionY * directionY);
        if (directionLength > 0)
        {
            directionX /= directionLength;
            directionY /= directionLength;
        }

        playerX += directionX * playerSpeed * deltaTime;
        playerY += directionY * playerSpeed * deltaTime;
        playerX = Math.Clamp(playerX, 0, screenWidth - playerSize);
        playerY = Math.Clamp(playerY, 0, screenHeight - playerSize);

        // Aim from the hazard's center toward the player's center.
        float chaseX = (playerX + playerSize / 2f) - (hazardX + hazardSize / 2f);
        float chaseY = (playerY + playerSize / 2f) - (hazardY + hazardSize / 2f);
        float chaseDistance = MathF.Sqrt(chaseX * chaseX + chaseY * chaseY);
        float hazardSpeed = MathF.Min(initialHazardSpeed + hazardAcceleration * survivalTime, maxHazardSpeed);
        if (chaseDistance > 0)
        {
            hazardX += chaseX / chaseDistance * hazardSpeed * deltaTime;
            hazardY += chaseY / chaseDistance * hazardSpeed * deltaTime;
        }

        Rectangle player = new(playerX, playerY, playerSize, playerSize);
        Rectangle hazard = new(hazardX, hazardY, hazardSize, hazardSize);
        if (Raylib.IsMouseButtonPressed(MouseButton.Left))
        {
            Vector2 start = new(playerX + playerSize / 2f, playerY + playerSize / 2f);
            Vector2 aim = Raylib.GetMousePosition() - start;
            if (aim.LengthSquared() > 0)
            {
                Vector2 velocity = Vector2.Normalize(aim) * projectileSpeed;
                projectiles.Add(new Projectile(start, velocity));
                Raylib.PlaySound(laserSound);
            }
        }

        // Iterate backwards so removing a projectile doesn't skip the next one.
        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            Projectile projectile = projectiles[i];
            projectile.Position += projectile.Velocity * deltaTime;
            if (Raylib.CheckCollisionCircleRec(projectile.Position, projectileRadius, hazard))
            {
                score++;
                projectiles.RemoveAt(i);
            }
            else if (projectile.Position.X < -projectileRadius || projectile.Position.X > screenWidth + projectileRadius ||
                     projectile.Position.Y < -projectileRadius || projectile.Position.Y > screenHeight + projectileRadius)
            {
                projectiles.RemoveAt(i);
            }
            else
            {
                projectiles[i] = projectile;
            }
        }

        if (Raylib.CheckCollisionRecs(player, hazard))
        {
            gameOver = true;
            Raylib.PlaySound(deathSound);
            bestScore = Math.Max(bestScore, score);
        }
    }
    else if (Raylib.IsKeyPressed(KeyboardKey.R))
    {
        playerX = 380;
        playerY = 205;
        hazardX = 0;
        hazardY = 100;
        survivalTime = 0;
        score = 0;
        projectiles.Clear();
        gameOver = false;
    }

    Raylib.BeginDrawing();
    Raylib.ClearBackground(Color.Black);
    Raylib.DrawRectangle((int)playerX, (int)playerY, playerSize, playerSize, Color.Green);
    Raylib.DrawRectangle((int)hazardX, (int)hazardY, hazardSize, hazardSize, Color.Red);
    foreach (Projectile projectile in projectiles)
    {
        Raylib.DrawCircleV(projectile.Position, projectileRadius, Color.Blue);
    }
    Raylib.DrawText($"Hits: {score}", 20, 20, 24, Color.White);
    Raylib.DrawText($"Best: {bestScore}", 20, 50, 24, Color.White);
    if (gameOver) Raylib.DrawText("Game over! Press R to restart", 200, 20, 24, Color.White);
    Vector2 mouse = Raylib.GetMousePosition();
    Raylib.DrawTexture(target, (int)(mouse.X - target.Width / 2f), (int)(mouse.Y - target.Height / 2f), Color.White);
    Raylib.EndDrawing();

    Console.WriteLine("Test");
}

Raylib.UnloadSound(laserSound);
Raylib.UnloadSound(deathSound);
Raylib.UnloadMusicStream(music);
Raylib.CloseAudioDevice();
Raylib.UnloadTexture(target);
Raylib.CloseWindow();

struct Projectile
{
    public Vector2 Position;
    public Vector2 Velocity;

    public Projectile(Vector2 position, Vector2 velocity)
    {
        Position = position;
        Velocity = velocity;
    }
}
