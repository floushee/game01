using Engine.Input;

namespace Game.Input;

sealed class GameInputMapper(IInput input)
{
    public GameInput Read()
    {
        float direction = 0;
        if (input.IsKeyDown(Key.D) || input.IsKeyDown(Key.Right)) direction++;
        if (input.IsKeyDown(Key.A) || input.IsKeyDown(Key.Left)) direction--;
        return new GameInput(direction, input.IsKeyPressed(Key.Space), input.IsKeyDown(Key.Space),
            input.GetMousePosition(),
            input.IsMouseButtonPressed(MouseButton.Left), input.IsKeyPressed(Key.R));
    }
}
