using CosmicSling.Domain.Entities;
using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Application.Commands;

public class LaunchShipCommand(Spaceship ship, Vector2D launchVelocity) : IGameCommand
{
    private Vector2D _previousPosition;
    private Vector2D _previousVelocity;

    public void Execute()
    {
        _previousPosition = ship.Position;
        _previousVelocity = ship.Velocity;
        ship.Velocity = launchVelocity;
    }

    public void Undo()
    {
        ship.Position = _previousPosition;
        ship.Velocity = _previousVelocity;
        ship.TrailPoints.Clear();
    }
}
