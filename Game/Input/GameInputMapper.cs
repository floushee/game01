using System.Numerics;
using Engine.Input;

namespace Game.Input;

sealed class GameInputMapper(IInput input)
{
    public GameInput Read()
    {
        Vector2 direction = Vector2.Zero;
        if (input.IsKeyDown(Key.D) || input.IsKeyDown(Key.Right)) direction.X++;
        if (input.IsKeyDown(Key.A) || input.IsKeyDown(Key.Left)) direction.X--;
        if (input.IsKeyDown(Key.S) || input.IsKeyDown(Key.Down)) direction.Y++;
        if (input.IsKeyDown(Key.W) || input.IsKeyDown(Key.Up)) direction.Y--;
        return new GameInput(direction, input.GetMousePosition(),
            input.IsMouseButtonPressed(MouseButton.Left), input.IsKeyPressed(Key.R));
    }
}
