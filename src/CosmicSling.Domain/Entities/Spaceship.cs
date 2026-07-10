using System.Collections.Generic;
using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Domain.Entities;

public class Spaceship : Entity
{
    public Vector2D Velocity { get; set; }
    public float Radius { get; set; } = 14f;
    public float Mass { get; set; } = 1f;
    public Vector2D StartPosition { get; private set; }
    public List<Vector2D> TrailPoints { get; } = new();

    public Spaceship(int id, Vector2D startPosition) : base(id, startPosition)
    {
        StartPosition = startPosition;
        Velocity = Vector2D.Zero;
    }

    public CircleBounds GetBounds() => new(Position, Radius);

    public void ResetState()
    {
        Position = StartPosition;
        Velocity = Vector2D.Zero;
        TrailPoints.Clear();
        IsActive = true;
    }

    public void RecordTrailPoint()
    {
        TrailPoints.Add(Position);
        if (TrailPoints.Count > 100)
        {
            TrailPoints.RemoveAt(0);
        }
    }
}
