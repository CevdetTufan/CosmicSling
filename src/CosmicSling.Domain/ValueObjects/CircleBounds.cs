namespace CosmicSling.Domain.ValueObjects;

public readonly record struct CircleBounds(Vector2D Center, float Radius)
{
    public bool Contains(Vector2D point)
    {
        return Vector2D.DistanceSquared(Center, point) <= Radius * Radius;
    }

    public bool Intersects(CircleBounds other)
    {
        var radiusSum = Radius + other.Radius;
        return Vector2D.DistanceSquared(Center, other.Center) <= radiusSum * radiusSum;
    }
}
