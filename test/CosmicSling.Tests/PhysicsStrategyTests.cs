using CosmicSling.Application.Strategies;
using CosmicSling.Domain.Entities;
using CosmicSling.Domain.Enums;
using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Tests;

public class PhysicsStrategyTests
{
    [Fact]
    public void NewtonianGravityStrategy_AttractsShipTowardsPlanet()
    {
        var ship = new Spaceship(1, new Vector2D(100f, 100f));
        var planet = new CelestialBody(2, new Vector2D(200f, 100f), 30f, 500f, 10f, CelestialType.Planet, GameColor.NeonBlue);

        var strategy = new NewtonianGravityStrategy();
        var force = strategy.CalculateForce(ship, planet);

        // Planet is to the right (+X), so force.X should be positive and force.Y should be 0
        Assert.True(force.X > 0f);
        Assert.Equal(0f, force.Y, precision: 4);
    }

    [Fact]
    public void NewtonianGravityStrategy_OutsideGravityRadius_ReturnsZeroForce()
    {
        var ship = new Spaceship(1, new Vector2D(0f, 0f));
        var planet = new CelestialBody(2, new Vector2D(1000f, 0f), 30f, 100f, 10f, CelestialType.Planet, GameColor.NeonBlue);

        var strategy = new NewtonianGravityStrategy();
        var force = strategy.CalculateForce(ship, planet);

        Assert.Equal(Vector2D.Zero, force);
    }

    [Fact]
    public void RepulsionFieldStrategy_PushesShipAway()
    {
        var ship = new Spaceship(1, new Vector2D(100f, 100f));
        var repulsor = new CelestialBody(2, new Vector2D(200f, 100f), 30f, 500f, 10f, CelestialType.Repulsor, GameColor.NeonOrange);

        var strategy = new RepulsionFieldStrategy();
        var force = strategy.CalculateForce(ship, repulsor);

        // Repulsor is to the right (+X), so force pushes ship to the left (-X)
        Assert.True(force.X < 0f);
    }
}
