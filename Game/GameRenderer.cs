using Engine.Rendering;
using System.Numerics;
using Engine;

namespace Game;

sealed class GameRenderer(IRenderer renderer)
{
    public void Draw(Player player, Hazard hazard, IReadOnlyList<Projectile> projectiles,
        int score, int bestScore, bool gameOver, ITexture target, Vector2 mouse)
    {
        renderer.BeginDrawing();
        renderer.ClearBackground(Color.Black);
        DrawPlayer(player);
        DrawHazard(hazard);
        foreach (Projectile projectile in projectiles) DrawProjectile(projectile);
        renderer.DrawText($"Hits: {score}", 20, 20, 24, Color.White);
        renderer.DrawText($"Best: {bestScore}", 20, 50, 24, Color.White);
        if (gameOver) renderer.DrawText("Game over! Press R to restart", 200, 20, 24, Color.White);
        renderer.DrawTexture(target, (int)(mouse.X - target.Width / 2f), (int)(mouse.Y - target.Height / 2f), Color.White);
        renderer.EndDrawing();
    }

    private void DrawPlayer(Player player) =>
        renderer.DrawRectangle((int)player.Bounds.X, (int)player.Bounds.Y, (int)player.Bounds.Width, (int)player.Bounds.Height, Color.Green);

    private void DrawHazard(Hazard hazard) =>
        renderer.DrawRectangle((int)hazard.Bounds.X, (int)hazard.Bounds.Y, (int)hazard.Bounds.Width, (int)hazard.Bounds.Height, Color.Red);

    private void DrawProjectile(Projectile projectile) =>
        renderer.DrawCircleV(projectile.Position, Projectile.Radius, Color.Blue);
}
