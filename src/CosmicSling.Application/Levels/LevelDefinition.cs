using CosmicSling.Domain.Entities;

namespace CosmicSling.Application.Levels;

public record LevelDefinition(
    int LevelIndex,
    string Name,
    string Hint,
    Spaceship Ship,
    Portal Portal,
    IReadOnlyList<CelestialBody> CelestialBodies,
    IReadOnlyList<Obstacle> Obstacles
);
