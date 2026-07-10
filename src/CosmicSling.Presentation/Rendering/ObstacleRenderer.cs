using SkiaSharp;
using CosmicSling.Domain.Entities;

namespace CosmicSling.Presentation.Rendering;

public class ObstacleRenderer : IEntityRenderer<Obstacle>
{
    public void Render(SKCanvas canvas, Obstacle obstacle)
    {
        using var paint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = new SKColor(obstacle.Color.R, obstacle.Color.G, obstacle.Color.B),
            IsAntialias = true
        };
        canvas.DrawCircle(obstacle.Position.X, obstacle.Position.Y, obstacle.Radius, paint);
    }
}
