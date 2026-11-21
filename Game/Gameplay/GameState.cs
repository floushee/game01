using Game.Input;
using System.Numerics;
using Engine.Mathematics;

namespace Game.Gameplay;

sealed class GameState
{
    private const float ProjectileSpeed = 500;

    private readonly Player player = new(new((Constants.ScreenWidth - Player.Size) / 2f, Constants.GroundY - Player.Size));

    private readonly Enemy enemy = new();

    private readonly List<Projectile> projectiles = [];

    private bool gameOver;

    private float survivalTime;

    private int score;

    private int bestScore;

    public GameEvents Update(float deltaTime, GameInput input)
    {
        if (gameOver)
        {
            if (input.Restart) Reset();
            return GameEvents.None;
        }

        survivalTime += deltaTime;
        GameEvents events = GameEvents.None;
        player.Update(input.MoveDirection, input.Jump, input.JumpHeld, deltaTime);
        enemy.Update(deltaTime, survivalTime, player.Center);

        if (input.Shoot)
        {
            Vector2 aim = input.PlayerAim - player.Center;
            if (aim.LengthSquared() > 0)
            {
                projectiles.Add(new Projectile(player.Center, Vector2.Normalize(aim) * ProjectileSpeed));
                events |= GameEvents.Shot;
            }
        }

        // Iterate backwards so removing a projectile doesn't skip the next one.
        for (int i = projectiles.Count - 1; i >= 0; i--)
        {
            Projectile projectile = projectiles[i];
            projectile.Update(deltaTime);
            if (Geometry.CheckCollisionCircleRec(projectile.Position, Projectile.Radius, enemy.Bounds))
            {
                score++;
                projectiles.RemoveAt(i);
            }
            else if (projectile.IsOutside(Constants.ScreenWidth, Constants.ScreenHeight))
            {
                projectiles.RemoveAt(i);
            }
            else
            {
                projectiles[i] = projectile;
            }
        }

        if (Geometry.CheckCollisionRecs(player.Bounds, enemy.Bounds))
        {
            gameOver = true;
            events |= GameEvents.Death;
            bestScore = Math.Max(bestScore, score);
        }

        return events;
    }

    private void Reset()
    {
        player.Reset();
        enemy.Reset();
        survivalTime = 0;
        score = 0;
        projectiles.Clear();
        gameOver = false;
    }

    public Player Player => player;

    public Enemy Enemy => enemy;

    public IReadOnlyList<Projectile> Projectiles => projectiles;

    public int Score => score;

    public int BestScore => bestScore;

    public bool GameOver => gameOver;
}
