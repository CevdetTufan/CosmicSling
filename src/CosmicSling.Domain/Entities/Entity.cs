using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Domain.Entities;

public abstract class Entity
{
    public int Id { get; init; }
    public Vector2D Position { get; set; }
    public bool IsActive { get; set; } = true;

    protected Entity(int id, Vector2D initialPosition)
    {
        Id = id;
        Position = initialPosition;
    }
}
