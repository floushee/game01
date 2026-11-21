using Game.Gameplay;
using Game.Resources;
using Engine.Rendering;
using System.Numerics;

namespace Game.Rendering;

sealed class GameRenderer(IRenderer renderer)
{
    private const float PixelsPerWalkFrame = 16;
    private const float EnemyFramesPerSecond = 12;

    private readonly LevelRenderer level = new(renderer);

    public void Draw(GameState state, GameResources resources, Vector2 mouse)
    {
        renderer.BeginDrawing();

        level.Draw();

        float enemyCenterX = state.Enemy.Position.X + state.Enemy.Bounds.Width / 2f;
        bool faceLeft = enemyCenterX < state.Player.Center.X;
        DrawSprite(SelectPlayerFrame(state.Player, resources.PlayerSprites), state.Player.Position, faceLeft);

        DrawSprite(SelectEnemyFrame(state.SurvivalTime, resources.EnemySprites), state.Enemy.Position);

        foreach (Projectile projectile in state.Projectiles) DrawProjectile(projectile);

        renderer.DrawText($"Hits: {state.Score}", 20, 20, 24, Color.White);
        renderer.DrawText($"Best: {state.BestScore}", 20, 50, 24, Color.White);
        if (state.GameOver) renderer.DrawText("Game over! Press R to restart", 200, 20, 24, Color.White);

        ITexture crosshair = resources.Crosshair;
        DrawSprite(crosshair, mouse - new Vector2(crosshair.Width / 2f, crosshair.Height / 2f));

        renderer.EndDrawing();
    }

    private static ITexture SelectPlayerFrame(Player player, PlayerSpriteSet sprites)
    {
        if (!player.IsGrounded)
            return player.VerticalVelocity < 0 ? sprites.Rise : sprites.Fall;

        if (player.WalkDistance == 0) return sprites.Idle;

        int frame = (int)(player.WalkDistance / PixelsPerWalkFrame) % 4;
        return frame switch
        {
            0 => sprites.WalkA,
            2 => sprites.WalkB,
            _ => sprites.Idle
        };
    }

    private static ITexture SelectEnemyFrame(float time, EnemySpriteSet sprites)
    {
        int frame = (int)(time * EnemyFramesPerSecond) % sprites.FlightFrames.Count;
        return sprites.FlightFrames[frame];
    }

    private void DrawSprite(ITexture texture, Vector2 position, bool flipHorizontally = false) =>
        renderer.DrawTexture(texture, (int)position.X, (int)position.Y, Color.White, flipHorizontally);

    private void DrawProjectile(Projectile projectile) =>
        renderer.DrawCircleV(projectile.Position, Projectile.Radius, Color.Blue);
}
