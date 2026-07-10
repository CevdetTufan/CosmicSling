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
        // 1. Draw glowing trail
        if (ship.TrailPoints.Count > 1)
        {
            using var path = new SKPath();
            path.MoveTo(ship.TrailPoints[0].X, ship.TrailPoints[0].Y);
            for (int i = 1; i < ship.TrailPoints.Count; i++)
            {
                path.LineTo(ship.TrailPoints[i].X, ship.TrailPoints[i].Y);
            }
            canvas.DrawPath(path, _trailPaint);
        }

        // 2. Draw outer glow
        canvas.DrawCircle(ship.Position.X, ship.Position.Y, ship.Radius * 1.6f, _shipGlowPaint);

        // 3. Draw inner core
        canvas.DrawCircle(ship.Position.X, ship.Position.Y, ship.Radius, _shipCorePaint);
    }
}
