using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Domain.Entities;

public class Spaceship(int id, Vector2D startPosition) : Entity(id, startPosition)
{
	public Vector2D Velocity { get; set; } = Vector2D.Zero;
	public float Radius { get; set; } = 14f;
    public float Mass { get; set; } = 1f;
	public Vector2D StartPosition { get; private set; } = startPosition;
	public List<Vector2D> TrailPoints { get; } = [];

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
