using SkiaSharp;
using CosmicSling.Domain.Entities;
using CosmicSling.Domain.Enums;

namespace CosmicSling.Presentation.Rendering;

public class CelestialRenderer : IEntityRenderer<CelestialBody>
{
    private readonly SKFont _labelFont = new(SKTypeface.Default, 13f);

    public void Render(SKCanvas canvas, CelestialBody body)
    {
        var baseColor = new SKColor(body.Color.R, body.Color.G, body.Color.B, body.Color.A);

        // 1. Draw gravitational field zone
        using var gravityPaint = new SKPaint
        {
            Style = SKPaintStyle.Stroke,
            Color = baseColor.WithAlpha(50),
            StrokeWidth = 1.5f,
            IsAntialias = true,
            PathEffect = SKPathEffect.CreateDash(new[] { 8f, 6f }, 0f)
        };
        canvas.DrawCircle(body.Position.X, body.Position.Y, body.GravityRadius, gravityPaint);

        // 2. Draw planet atmospheric glow
        using var glowPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = baseColor.WithAlpha(45),
            IsAntialias = true
        };
        canvas.DrawCircle(body.Position.X, body.Position.Y, body.Radius * 1.4f, glowPaint);

        // 3. Draw planet body
        using var bodyPaint = new SKPaint
        {
            Style = SKPaintStyle.Fill,
            Color = baseColor,
            IsAntialias = true
        };
        canvas.DrawCircle(body.Position.X, body.Position.Y, body.Radius, bodyPaint);

        // 4. Draw label or icon if Repulsor / BlackHole
        if (body.Type != CelestialType.Planet)
        {
            using var textPaint = new SKPaint
            {
                Color = SKColors.White,
                IsAntialias = true
            };
            string label = body.Type == CelestialType.Repulsor ? "REPULSION" : "BLACK HOLE";
            canvas.DrawText(label, body.Position.X, body.Position.Y + 4f, SKTextAlign.Center, _labelFont, textPaint);
        }
    }
}
