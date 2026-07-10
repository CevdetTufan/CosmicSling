---
name: modern-dotnet-skiasharp-guidelines
description: Enforces clean, non-deprecated APIs for .NET 10 and SkiaSharp development in Cosmic Sling. Use when writing or refactoring rendering and game engine code.
---

# Modern .NET 10 & SkiaSharp Guidelines

When adding or refactoring code in `CosmicSling`:

1. **Zero Deprecated API Usage (`CS0618`)**
   - Inspect all SkiaSharp calls and verify none are obsolete (`[Obsolete]`).
   - Use `canvas.DrawLine(x0, y0, x1, y1, paint)` for segment paths or lines.
   - Use `SKFont` (`canvas.DrawText(text, x, y, textAlign, font, paint)`) instead of setting font attributes directly on `SKPaint`.

2. **Zero Compiler Warnings Policy**
   - Do not suppress `CS0618` via pragmas or `.csproj` `<NoWarn>`.
   - Every `dotnet build` execution must output precisely:
     ```text
     0 Warning(s)
     0 Error(s)
     ```
