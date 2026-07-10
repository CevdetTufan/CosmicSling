using CosmicSling.Domain.ValueObjects;
using SkiaSharp;

namespace CosmicSling.Presentation.Rendering;

public class TrajectoryRenderer
{
    private readonly SKPaint _paint = new()
    {
        Style = SKPaintStyle.Stroke,
        Color = new SKColor(255, 255, 0, 180),
        StrokeWidth = 2f,
        IsAntialias = true,
        PathEffect = SKPathEffect.CreateDash(new[] { 6f, 6f }, 0f)
    };

    public void Render(SKCanvas canvas, IReadOnlyList<Vector2D> points)
    {
        if (points.Count < 2)
        {
            return;
        }

        for (int i = 0; i < points.Count - 1; i++)
        {
            var p1 = points[i];
            var p2 = points[i + 1];
            canvas.DrawLine(p1.X, p1.Y, p2.X, p2.Y, _paint);
        }
    }
}
