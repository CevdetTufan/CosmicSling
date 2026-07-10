namespace CosmicSling.Infrastructure.Persistence;

public class InMemoryHighScoreRepository : IHighScoreRepository
{
    private readonly Dictionary<int, int> _bestAttempts = [];

    public void SaveBestAttempts(int levelIndex, int attempts)
    {
        if (!_bestAttempts.TryGetValue(levelIndex, out var currentBest) || attempts < currentBest)
        {
            _bestAttempts[levelIndex] = attempts;
        }
    }

    public int GetBestAttempts(int levelIndex)
    {
        return _bestAttempts.TryGetValue(levelIndex, out var best) ? best : 0;
    }
}
