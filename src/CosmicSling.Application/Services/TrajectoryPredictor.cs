using CosmicSling.Domain.Entities;
using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Application.Services;

public class TrajectoryPredictor
{
    private readonly PhysicsEngineService _physicsEngine = new();

    public IReadOnlyList<Vector2D> PredictTrajectory(
        Spaceship originalShip,
        Vector2D initialVelocity,
        IReadOnlyList<CelestialBody> celestialBodies,
        int steps = 45,
        float dt = 0.033f)
    {
        var predictedPoints = new List<Vector2D>(steps);

        var tempShip = new Spaceship(-1, originalShip.Position)
        {
            Velocity = initialVelocity,
            Mass = originalShip.Mass,
            Radius = originalShip.Radius
        };

        for (int i = 0; i < steps; i++)
        {
            _physicsEngine.Step(tempShip, celestialBodies, dt);
            predictedPoints.Add(tempShip.Position);
        }

        return predictedPoints;
    }
}
