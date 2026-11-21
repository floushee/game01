using System.Numerics;

namespace Game.Input;

readonly record struct GameInput(Vector2 PlayerDirection, Vector2 PlayerAim, bool Shoot, bool Restart);
