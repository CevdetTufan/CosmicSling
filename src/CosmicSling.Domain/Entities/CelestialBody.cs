using CosmicSling.Domain.Enums;
using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Domain.Entities;

public class CelestialBody(
	int id,
	Vector2D position,
	float radius,
	float gravityRadius,
	float mass,
	CelestialType type,
	GameColor color) : Entity(id, position)
{
	public float Radius { get; set; } = radius;
	public float GravityRadius { get; set; } = gravityRadius;
	public float Mass { get; set; } = mass;
	public CelestialType Type { get; set; } = type;
	public GameColor Color { get; set; } = color;

	public CircleBounds GetBodyBounds() => new(Position, Radius);
    public CircleBounds GetGravityBounds() => new(Position, GravityRadius);
}
