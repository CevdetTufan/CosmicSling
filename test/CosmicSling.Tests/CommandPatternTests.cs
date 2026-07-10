using CosmicSling.Application.Commands;
using CosmicSling.Domain.Entities;
using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Tests;

public class CommandPatternTests
{
    [Fact]
    public void LaunchShipCommand_ExecuteAndUndo_WorksCorrectly()
    {
        var startPos = new Vector2D(50f, 50f);
        var ship = new Spaceship(1, startPos);
        var launchVelocity = new Vector2D(100f, -50f);

        var command = new LaunchShipCommand(ship, launchVelocity);

        command.Execute();
        Assert.Equal(launchVelocity, ship.Velocity);

        command.Undo();
        Assert.Equal(Vector2D.Zero, ship.Velocity);
        Assert.Equal(startPos, ship.Position);
    }
}
