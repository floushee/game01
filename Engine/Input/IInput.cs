using System.Numerics;

namespace Engine.Input;

public interface IInput
{
    bool IsKeyDown(Key key);

    bool IsKeyPressed(Key key);

    bool IsMouseButtonPressed(MouseButton button);

    Vector2 GetMousePosition();
}
