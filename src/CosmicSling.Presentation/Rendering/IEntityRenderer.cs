using SkiaSharp;

namespace CosmicSling.Presentation.Rendering;

public interface IEntityRenderer<in TEntity>
{
    void Render(SKCanvas canvas, TEntity entity);
}
