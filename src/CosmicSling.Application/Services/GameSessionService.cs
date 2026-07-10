using CosmicSling.Application.Commands;
using CosmicSling.Application.Levels;
using CosmicSling.Domain.Enums;
using CosmicSling.Domain.Interfaces;
using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Application.Services;

public class GameSessionService
{
    private readonly PhysicsEngineService _physicsEngine = new();
    private readonly TrajectoryPredictor _trajectoryPredictor = new();
    private readonly Stack<IGameCommand> _commandHistory = new();
    private readonly List<IGameEventListener> _listeners = [];

    public LevelDefinition CurrentLevel { get; private set; } = null!;
    public GameState CurrentState { get; private set; } = GameState.Aiming;
    public int Attempts { get; private set; }
    public string StatusMessage { get; private set; } = string.Empty;

    public void SubscribeListener(IGameEventListener listener) => _listeners.Add(listener);

    public void LoadLevel(int levelIndex)
    {
        CurrentLevel = LevelFactory.CreateLevel(levelIndex);
        CurrentState = GameState.Aiming;
        StatusMessage = CurrentLevel.Hint;
        _commandHistory.Clear();
    }

    public void LaunchShip(Vector2D launchVelocity)
    {
        if (CurrentState != GameState.Aiming)
        {
            return;
        }

        var command = new LaunchShipCommand(CurrentLevel.Ship, launchVelocity);
        command.Execute();
        _commandHistory.Push(command);

        CurrentState = GameState.Flying;
        Attempts++;

        foreach (var listener in _listeners)
        {
            listener.OnShipLaunched();
        }
    }

    public void UndoLastCommand()
    {
        if (_commandHistory.Count > 0)
        {
            var lastCommand = _commandHistory.Pop();
            lastCommand.Undo();
            CurrentState = GameState.Aiming;
            StatusMessage = "Son hamle geri alındı (Command Undo).";
        }
    }

    public void ResetLevel()
    {
        CurrentLevel.Ship.ResetState();
        CurrentState = GameState.Aiming;
        StatusMessage = CurrentLevel.Hint;
    }

    public IReadOnlyList<Vector2D> GetPredictedTrajectory(Vector2D launchVelocity)
    {
        return _trajectoryPredictor.PredictTrajectory(
            CurrentLevel.Ship,
            launchVelocity,
            CurrentLevel.CelestialBodies);
    }

    public void Update(float deltaTime)
    {
        if (CurrentState != GameState.Flying)
        {
            return;
        }

        _physicsEngine.Step(CurrentLevel.Ship, CurrentLevel.CelestialBodies, deltaTime);

        var collisionResult = CollisionDetectionService.CheckCollisions(CurrentLevel.Ship, CurrentLevel);

        switch (collisionResult)
        {
            case CollisionResult.PortalReached:
                CurrentState = GameState.LevelCompleted;
                StatusMessage = "TEBRİKLER! Portal başarıyla geçildi!";
                foreach (var l in _listeners) l.OnLevelCompleted();
                break;

            case CollisionResult.PlanetImpact:
                CurrentState = GameState.GameOver;
                StatusMessage = "KAZA! Gezegene çarptınız. [R] ile tekrar deneyin.";
                foreach (var l in _listeners) l.OnGameOver("PlanetImpact");
                break;

            case CollisionResult.ObstacleImpact:
                CurrentState = GameState.GameOver;
                StatusMessage = "KAZA! Engele çarptınız. [R] ile tekrar deneyin.";
                foreach (var l in _listeners) l.OnGameOver("ObstacleImpact");
                break;

            case CollisionResult.OutOfBounds:
                CurrentState = GameState.GameOver;
                StatusMessage = "YÖRÜNGEDEN ÇIKILDI! [R] ile tekrar deneyin.";
                foreach (var l in _listeners) l.OnGameOver("OutOfBounds");
                break;
        }
    }
}
