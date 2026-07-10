using CosmicSling.Domain.Entities;
using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Domain.Interfaces;

public interface IPhysicsStrategy
{
    Vector2D CalculateForce(Spaceship ship, CelestialBody body);
}
