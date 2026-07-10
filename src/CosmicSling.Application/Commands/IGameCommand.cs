namespace CosmicSling.Application.Commands;

public interface IGameCommand
{
    void Execute();
    void Undo();
}
