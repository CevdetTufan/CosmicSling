using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Domain.Entities;

public class Portal : Entity
{
    public float Radius { get; set; } = 28f;
    public GameColor Color { get; set; } = GameColor.NeonGreen;

    public Portal(int id, Vector2D position) : base(id, position)
    {
    }

    public CircleBounds GetBounds() => new(Position, Radius);
}
