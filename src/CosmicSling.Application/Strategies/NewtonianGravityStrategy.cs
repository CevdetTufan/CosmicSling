using System;
using CosmicSling.Domain.Entities;
using CosmicSling.Domain.Interfaces;
using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Application.Strategies;

public class NewtonianGravityStrategy : IPhysicsStrategy
{
    private const float GravitationalConstant = 25000f;
    private const float MinDistanceSquared = 900f; // 30 pixels minimum clamped distance

    public Vector2D CalculateForce(Spaceship ship, CelestialBody body)
    {
        var direction = body.Position - ship.Position;
        var distSq = direction.LengthSquared;

        // Only apply force within GravityRadius
        if (distSq > body.GravityRadius * body.GravityRadius)
        {
            return Vector2D.Zero;
        }

        var clampedDistSq = MathF.Max(distSq, MinDistanceSquared);
        var forceMagnitude = (GravitationalConstant * body.Mass * ship.Mass) / clampedDistSq;

        return direction.Normalize() * forceMagnitude;
    }
}
