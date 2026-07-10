using Xunit;
using CosmicSling.Application.Levels;
using CosmicSling.Application.Services;

namespace CosmicSling.Tests;

public class CollisionDetectionTests
{
    [Fact]
    public void CheckCollisions_WhenShipReachesPortal_ReturnsPortalReached()
    {
        var level = LevelFactory.CreateLevel(1);
        level.Ship.Position = level.Portal.Position; // Move ship inside portal

        var service = new CollisionDetectionService();
        var result = service.CheckCollisions(level.Ship, level);

        Assert.Equal(CollisionResult.PortalReached, result);
    }

    [Fact]
    public void CheckCollisions_WhenShipHitsPlanet_ReturnsPlanetImpact()
    {
        var level = LevelFactory.CreateLevel(1);
        level.Ship.Position = level.CelestialBodies[0].Position; // Move ship inside planet core

        var service = new CollisionDetectionService();
        var result = service.CheckCollisions(level.Ship, level);

        Assert.Equal(CollisionResult.PlanetImpact, result);
    }
}
