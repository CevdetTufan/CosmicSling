using System.Linq;
using CosmicSling.Application.Levels;
using CosmicSling.Domain.Entities;

namespace CosmicSling.Application.Services;

public enum CollisionResult
{
    None,
    PortalReached,
    PlanetImpact,
    ObstacleImpact,
    OutOfBounds
}

public static class CollisionDetectionService
{
    public static CollisionResult CheckCollisions(Spaceship ship, LevelDefinition level, float screenWidth = 1200f, float screenHeight = 800f)
    {
        if (!ship.IsActive)
        {
            return CollisionResult.None;
        }

        var shipBounds = ship.GetBounds();

        // 1. Check Portal winning condition
        if (shipBounds.Intersects(level.Portal.GetBounds()))
        {
            return CollisionResult.PortalReached;
        }

        // 2. Check Planet collision (crashing into planet core)
        if (level.CelestialBodies.Any(body => shipBounds.Intersects(body.GetBodyBounds())))
        {
            return CollisionResult.PlanetImpact;
        }

        // 3. Check Obstacle collision
        if (level.Obstacles.Any(obstacle => shipBounds.Intersects(obstacle.GetBounds())))
        {
            return CollisionResult.ObstacleImpact;
        }

        // 4. Check Out of Bounds (> 400px outside screen)
        if (ship.Position.X < -400f || ship.Position.X > screenWidth + 400f ||
            ship.Position.Y < -400f || ship.Position.Y > screenHeight + 400f)
        {
            return CollisionResult.OutOfBounds;
        }

        return CollisionResult.None;
    }
}
