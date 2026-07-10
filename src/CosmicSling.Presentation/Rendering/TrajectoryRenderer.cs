using System.Collections.Generic;
using SkiaSharp;
using CosmicSling.Domain.ValueObjects;

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

        using var path = new SKPath();
        path.MoveTo(points[0].X, points[0].Y);
        for (int i = 1; i < points.Count; i++)
        {
            path.LineTo(points[i].X, points[i].Y);
        }
        canvas.DrawPath(path, _paint);
    }
}
