using SkiaSharp;
using CosmicSling.Domain.Entities;

namespace CosmicSling.Presentation.Rendering;

public class PortalRenderer : IEntityRenderer<Portal>
{
    public void Render(SKCanvas canvas, Portal portal)
    {
        var color = new SKColor(portal.Color.R, portal.Color.G, portal.Color.B);

        using var outerPaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = color,
            StrokeWidth = 3f,
            IsAntialias = true
        };

        using var innerPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = color.WithAlpha(70),
            IsAntialias = true
        };

        canvas.DrawCircle(portal.Position.X, portal.Position.Y, portal.Radius, outerPaint);
        canvas.DrawCircle(portal.Position.X, portal.Position.Y, portal.Radius * 0.7f, innerPaint);
    }
}
