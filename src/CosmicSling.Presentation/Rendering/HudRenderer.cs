using SkiaSharp;
using CosmicSling.Application.Services;

namespace CosmicSling.Presentation.Rendering;

public class HudRenderer
{
    private readonly SKFont _titleFont = new(SKTypeface.Default, 22f);
    private readonly SKFont _statusFont = new(SKTypeface.Default, 16f);
    private readonly SKFont _controlFont = new(SKTypeface.Default, 14f);

    public void Render(SKCanvas canvas, GameSessionService session, int bestAttempts)
    {
        using var titlePaint = new SKPaint
        {
            Color = new SKColor(0, 255, 255),
            IsAntialias = true
        };

        using var statusPaint = new SKPaint
        {
            Color = new SKColor(255, 255, 255),
            IsAntialias = true
        };

        using var controlPaint = new SKPaint
        {
            Color = new SKColor(180, 180, 180),
            IsAntialias = true
        };

        var level = session.CurrentLevel;
        canvas.DrawText($"{level.Name}   |   Deneme: {session.Attempts}   |   En İyi: {(bestAttempts == 0 ? "-" : bestAttempts)}", 24f, 36f, SKTextAlign.Left, _titleFont, titlePaint);
        canvas.DrawText($"Durum: {session.StatusMessage}", 24f, 64f, SKTextAlign.Left, _statusFont, statusPaint);

        canvas.DrawText("Fare Sürükle/Bırak: Fırlat  |  [R] Sıfırla  |  [Z] Geri Al (Undo)  |  [1] [2] [3] Bölüm Seç", 24f, 770f, SKTextAlign.Left, _controlFont, controlPaint);
    }
}
