using CosmicSling.Domain.Enums;
using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Domain.Entities;

public class CelestialBody : Entity
{
    public float Radius { get; set; }
    public float GravityRadius { get; set; }
    public float Mass { get; set; }
    public CelestialType Type { get; set; }
    public GameColor Color { get; set; }

    public CelestialBody(
        int id,
        Vector2D position,
        float radius,
        float gravityRadius,
        float mass,
        CelestialType type,
        GameColor color) : base(id, position)
    {
        Radius = radius;
        GravityRadius = gravityRadius;
        Mass = mass;
        Type = type;
        Color = color;
    }

    public CircleBounds GetBodyBounds() => new(Position, Radius);
    public CircleBounds GetGravityBounds() => new(Position, GravityRadius);
}
