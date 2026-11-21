using Game.Gameplay;
using Engine.Rendering;
using System.Numerics;

namespace Game.Rendering;

sealed class GameRenderer(IRenderer renderer)
{
    public void Draw(GameState state, ITexture crosshair, Vector2 mouse)
    {
        renderer.BeginDrawing();
        renderer.ClearBackground(Color.Black);
        DrawPlayer(state.Player);
        DrawEnemy(state.Enemy);
        foreach (Projectile projectile in state.Projectiles) DrawProjectile(projectile);
        renderer.DrawText($"Hits: {state.Score}", 20, 20, 24, Color.White);
        renderer.DrawText($"Best: {state.BestScore}", 20, 50, 24, Color.White);
        if (state.GameOver) renderer.DrawText("Game over! Press R to restart", 200, 20, 24, Color.White);
        renderer.DrawTexture(crosshair, (int)(mouse.X - crosshair.Width / 2f), (int)(mouse.Y - crosshair.Height / 2f), Color.White);
        renderer.EndDrawing();
    }

    private void DrawPlayer(Player player) =>
        renderer.DrawRectangle((int)player.Bounds.X, (int)player.Bounds.Y, (int)player.Bounds.Width, (int)player.Bounds.Height, Color.Green);

    private void DrawEnemy(Enemy enemy) =>
        renderer.DrawRectangle((int)enemy.Bounds.X, (int)enemy.Bounds.Y, (int)enemy.Bounds.Width, (int)enemy.Bounds.Height, Color.Red);

    private void DrawProjectile(Projectile projectile) =>
        renderer.DrawCircleV(projectile.Position, Projectile.Radius, Color.Blue);
}
