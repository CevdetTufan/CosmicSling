using CosmicSling.Domain.Entities;
using CosmicSling.Domain.Interfaces;
using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Application.Strategies;

public class RepulsionFieldStrategy : IPhysicsStrategy
{
    private const float RepulsionConstant = 18000f;
    private const float MinDistanceSquared = 900f;

    public Vector2D CalculateForce(Spaceship ship, CelestialBody body)
    {
        var direction = ship.Position - body.Position; // Away from the body
        var distSq = direction.LengthSquared;

        if (distSq > body.GravityRadius * body.GravityRadius)
        {
            return Vector2D.Zero;
        }

        var clampedDistSq = MathF.Max(distSq, MinDistanceSquared);
        var forceMagnitude = (RepulsionConstant * body.Mass * ship.Mass) / clampedDistSq;

        return direction.Normalize() * forceMagnitude;
    }
}
