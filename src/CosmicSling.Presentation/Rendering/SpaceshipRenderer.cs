using SkiaSharp;
using CosmicSling.Domain.Entities;

namespace CosmicSling.Presentation.Rendering;

public class SpaceshipRenderer : IEntityRenderer<Spaceship>
{
    private readonly SKPaint _trailPaint = new()
    {
        Style = SKPaintStyle.Stroke,
        Color = new SKColor(0, 255, 255, 120),
        StrokeWidth = 2.5f,
        IsAntialias = true
    };

    private readonly SKPaint _shipGlowPaint = new()
    {
        Style = SKPaintStyle.Fill,
        Color = new SKColor(0, 255, 255, 60),
        IsAntialias = true
    };

    private readonly SKPaint _shipCorePaint = new()
    {
        Style = SKPaintStyle.Fill,
        Color = new SKColor(0, 255, 255, 255),
        IsAntialias = true
    };

    public void Render(SKCanvas canvas, Spaceship ship)
    {
        // 1. Draw glowing trail using non-deprecated DrawLine segments
        if (ship.TrailPoints.Count > 1)
        {
            for (int i = 0; i < ship.TrailPoints.Count - 1; i++)
            {
                var p1 = ship.TrailPoints[i];
                var p2 = ship.TrailPoints[i + 1];
                canvas.DrawLine(p1.X, p1.Y, p2.X, p2.Y, _trailPaint);
            }
        }

        // 2. Draw outer glow
        canvas.DrawCircle(ship.Position.X, ship.Position.Y, ship.Radius * 1.6f, _shipGlowPaint);

        // 3. Draw inner core
        canvas.DrawCircle(ship.Position.X, ship.Position.Y, ship.Radius, _shipCorePaint);
    }
}
