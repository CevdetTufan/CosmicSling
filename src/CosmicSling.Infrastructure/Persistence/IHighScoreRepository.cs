namespace CosmicSling.Infrastructure.Persistence;

public interface IHighScoreRepository
{
    void SaveBestAttempts(int levelIndex, int attempts);
    int GetBestAttempts(int levelIndex);
}
