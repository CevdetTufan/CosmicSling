using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Tests;

public class Vector2DTests
{
    [Fact]
    public void VectorAddition_WorksCorrectly()
    {
        var v1 = new Vector2D(10f, 20f);
        var v2 = new Vector2D(5f, -5f);

        var result = v1 + v2;

        Assert.Equal(15f, result.X);
        Assert.Equal(15f, result.Y);
    }

    [Fact]
    public void Distance_CalculatesEuclideanDistance()
    {
        var a = new Vector2D(0f, 0f);
        var b = new Vector2D(3f, 4f);

        var dist = Vector2D.Distance(a, b);

        Assert.Equal(5f, dist);
    }

    [Fact]
    public void Normalize_ReturnsUnitVector()
    {
        var v = new Vector2D(10f, 0f);
        var norm = v.Normalize();

        Assert.Equal(1f, norm.X);
        Assert.Equal(0f, norm.Y);
        Assert.Equal(1f, norm.Length);
    }
}
