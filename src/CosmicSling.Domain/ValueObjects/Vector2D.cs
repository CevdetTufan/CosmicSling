using System;

namespace CosmicSling.Domain.ValueObjects;

public readonly record struct Vector2D(float X, float Y)
{
    public static Vector2D Zero => new(0f, 0f);

    public float Length => MathF.Sqrt(X * X + Y * Y);
    public float LengthSquared => X * X + Y * Y;

    public Vector2D Normalize()
    {
        var len = Length;
        return len > 0.0001f ? new Vector2D(X / len, Y / len) : Zero;
    }

    public static float Distance(Vector2D a, Vector2D b)
    {
        var dx = a.X - b.X;
        var dy = a.Y - b.Y;
        return MathF.Sqrt(dx * dx + dy * dy);
    }

    public static float DistanceSquared(Vector2D a, Vector2D b)
    {
        var dx = a.X - b.X;
        var dy = a.Y - b.Y;
        return dx * dx + dy * dy;
    }

    public static float Dot(Vector2D a, Vector2D b) => a.X * b.X + a.Y * b.Y;

    public static Vector2D operator +(Vector2D a, Vector2D b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector2D operator -(Vector2D a, Vector2D b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector2D operator -(Vector2D v) => new(-v.X, -v.Y);
    public static Vector2D operator *(Vector2D v, float scalar) => new(v.X * scalar, v.Y * scalar);
    public static Vector2D operator *(float scalar, Vector2D v) => new(v.X * scalar, v.Y * scalar);
    public static Vector2D operator /(Vector2D v, float scalar) => scalar != 0 ? new(v.X / scalar, v.Y / scalar) : Zero;
}
