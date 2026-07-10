namespace CosmicSling.Domain.Interfaces;

public interface IGameEventListener
{
    void OnShipLaunched();
    void OnLevelCompleted();
    void OnGameOver(string reason);
}
