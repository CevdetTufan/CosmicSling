using CosmicSling.Domain.Entities;
using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Application.Commands;

public class LaunchShipCommand : IGameCommand
{
    private readonly Spaceship _ship;
    private readonly Vector2D _launchVelocity;
    private Vector2D _previousPosition;
    private Vector2D _previousVelocity;

    public LaunchShipCommand(Spaceship ship, Vector2D launchVelocity)
    {
        _ship = ship;
        _launchVelocity = launchVelocity;
    }

    public void Execute()
    {
        _previousPosition = _ship.Position;
        _previousVelocity = _ship.Velocity;
        _ship.Velocity = _launchVelocity;
    }

    public void Undo()
    {
        _ship.Position = _previousPosition;
        _ship.Velocity = _previousVelocity;
        _ship.TrailPoints.Clear();
    }
}
