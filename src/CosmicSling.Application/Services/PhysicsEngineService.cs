using CosmicSling.Application.Strategies;
using CosmicSling.Domain.Entities;
using CosmicSling.Domain.Enums;
using CosmicSling.Domain.Interfaces;
using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Application.Services;

public class PhysicsEngineService
{
    private readonly IPhysicsStrategy _newtonianStrategy = new NewtonianGravityStrategy();
    private readonly IPhysicsStrategy _repulsionStrategy = new RepulsionFieldStrategy();

    public void Step(Spaceship ship, IReadOnlyList<CelestialBody> celestialBodies, float deltaTime)
    {
        if (!ship.IsActive)
        {
            return;
        }

        var totalForce = Vector2D.Zero;

        foreach (var body in celestialBodies)
        {
            var strategy = body.Type == CelestialType.Repulsor ? _repulsionStrategy : _newtonianStrategy;
            totalForce += strategy.CalculateForce(ship, body);
        }

        var acceleration = totalForce / ship.Mass;
        ship.Velocity += acceleration * deltaTime;
        ship.Position += ship.Velocity * deltaTime;

        ship.RecordTrailPoint();
    }
}
