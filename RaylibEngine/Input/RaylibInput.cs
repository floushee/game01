using System.Numerics;
using Engine.Input;
using Raylib_cs;

namespace RaylibEngine.Input;

internal sealed class RaylibInput : IInput
{
    public bool IsKeyDown(Key key) => Raylib.IsKeyDown(MapKey(key));

    public bool IsKeyPressed(Key key) => Raylib.IsKeyPressed(MapKey(key));

    public bool IsMouseButtonPressed(Engine.Input.MouseButton button) => Raylib.IsMouseButtonPressed(MapButton(button));

    private static KeyboardKey MapKey(Key key) => key switch
    {
        Key.R => KeyboardKey.R,
        Key.D => KeyboardKey.D,
        Key.Right => KeyboardKey.Right,
        Key.A => KeyboardKey.A,
        Key.Left => KeyboardKey.Left,
        Key.S => KeyboardKey.S,
        Key.Down => KeyboardKey.Down,
        Key.W => KeyboardKey.W,
        Key.Up => KeyboardKey.Up,
        _ => throw new ArgumentOutOfRangeException(nameof(key))
    };

    private static Raylib_cs.MouseButton MapButton(Engine.Input.MouseButton button) => button switch
    {
        Engine.Input.MouseButton.Left => Raylib_cs.MouseButton.Left,
        _ => throw new ArgumentOutOfRangeException(nameof(button))
    };

    public Vector2 GetMousePosition() => Raylib.GetMousePosition();
}
