using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Domain.Entities;

public class Obstacle : Entity
{
    public float Radius { get; set; }
    public GameColor Color { get; set; } = GameColor.NeonPink;

    public Obstacle(int id, Vector2D position, float radius) : base(id, position)
    {
        Radius = radius;
    }

    public CircleBounds GetBounds() => new(Position, Radius);
}
