using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Domain.Entities;

public class Obstacle(int id, Vector2D position, float radius) : Entity(id, position)
{
	public float Radius { get; set; } = radius;
	public GameColor Color { get; set; } = GameColor.NeonPink;

	public CircleBounds GetBounds() => new(Position, Radius);
}
