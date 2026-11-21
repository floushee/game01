using Engine.Mathematics;
using System.Numerics;

namespace Game.Gameplay;

class Player(Vector2 initialPosition)
{
    public const int Size = 40;

    private const float Speed = 200;       // px/s
    private const float JumpSpeed = 620;   // px/s
    private const float JumpCutSpeed = JumpSpeed / 2f;
    private const float Gravity = 1000;    // px/s²

    public Vector2 Position { get; private set; } = initialPosition;

    public float VerticalVelocity { get; private set; }

    public float WalkDistance { get; private set; }

    public bool IsGrounded { get; private set; } = true;

    public Vector2 Center => Position + new Vector2(Size / 2f);

    public Rectangle Bounds => new(Position.X, Position.Y, Size, Size);

    public void Update(float direction, bool jump, bool jumpHeld, float deltaTime)
    {
        Vector2 position = Position;
        position.X = Math.Clamp(position.X + direction * Speed * deltaTime, 0, Constants.ScreenWidth - Size);

        if (jump && IsGrounded)
        {
            // Screen Y increases downward, so a jump starts with negative velocity.
            VerticalVelocity = -JumpSpeed;
            IsGrounded = false;
        }

        if (!IsGrounded)
        {
            // Releasing jump early caps upward speed without affecting the fall.
            if (!jumpHeld && VerticalVelocity < -JumpCutSpeed)
            {
                VerticalVelocity = -JumpCutSpeed;
            }

            // Constant acceleration: displacement = v * t + ½ * a * t².
            position.Y += VerticalVelocity * deltaTime + 0.5f * Gravity * deltaTime * deltaTime;
            VerticalVelocity += Gravity * deltaTime;

            // The floor stops the fall when the player's feet reach it.
            if (VerticalVelocity >= 0 && position.Y + Size >= Constants.GroundY)
            {
                position.Y = Constants.GroundY - Size;
                VerticalVelocity = 0;
                IsGrounded = true;
            }
        }

        // Count actual movement so holding a direction against a wall doesn't animate walking.
        float horizontalTravel = MathF.Abs(position.X - Position.X);
        WalkDistance = IsGrounded && horizontalTravel > 0 ? WalkDistance + horizontalTravel : 0;
        Position = position;
    }

    public void Reset()
    {
        Position = new Vector2((Constants.ScreenWidth - Size) / 2f, Constants.GroundY - Size);
        VerticalVelocity = 0;
        WalkDistance = 0;
        IsGrounded = true;
    }
}
