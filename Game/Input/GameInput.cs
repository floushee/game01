using System.Numerics;

namespace Game.Input;

readonly record struct GameInput(float MoveDirection, bool Jump, bool JumpHeld, Vector2 PlayerAim, bool Shoot, bool Restart);
