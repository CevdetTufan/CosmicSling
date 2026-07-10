using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Domain.Entities;

public class Portal(int id, Vector2D position) : Entity(id, position)
{
    public float Radius { get; set; } = 28f;
    public GameColor Color { get; set; } = GameColor.NeonGreen;

	public CircleBounds GetBounds() => new(Position, Radius);
}
