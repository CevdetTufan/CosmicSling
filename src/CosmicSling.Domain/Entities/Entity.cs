using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Domain.Entities;

public abstract class Entity(int id, Vector2D initialPosition)
{
	public int Id { get; init; } = id;
	public Vector2D Position { get; set; } = initialPosition;
	public bool IsActive { get; set; } = true;
}
