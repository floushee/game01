using System.Numerics;
using Engine;

namespace Game;

struct Projectile
{
    public const float Radius = 5;

    public Vector2 Position { get; private set; }

    private readonly Vector2 velocity;

    public Projectile(Vector2 position, Vector2 velocity)
    {
        Position = position;
        this.velocity = velocity;
    }

    public void Update(float deltaTime) => Position += velocity * deltaTime;

    public bool IsOutside(int width, int height) =>
        Position.X < -Radius || Position.X > width + Radius ||
        Position.Y < -Radius || Position.Y > height + Radius;

}
