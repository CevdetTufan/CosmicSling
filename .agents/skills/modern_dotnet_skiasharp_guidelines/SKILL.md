---
name: modern-dotnet-skiasharp-guidelines
description: Enforces clean, non-deprecated APIs and modern C# / SonarQube rules (IDE0290, IDE0300, CA1822, CA1859, S1118, S1244, S1450, S3267) for .NET 10 and SkiaSharp development in Cosmic Sling. Use when writing or refactoring code.
---

# Modern .NET 10 & SkiaSharp Guidelines

When adding or refactoring code in `CosmicSling`:

1. **Zero Deprecated API Usage (`CS0618`)**
   - Inspect all SkiaSharp calls and verify none are obsolete (`[Obsolete]`).
   - Use `canvas.DrawLine(x0, y0, x1, y1, paint)` for segment paths or lines.
   - Use `SKFont` (`canvas.DrawText(text, x, y, textAlign, font, paint)`) instead of setting font attributes directly on `SKPaint`.

2. **Concrete Types for Performance (`CA1859`)**
   - Use concrete types instead of interfaces for private fields or local variables where polymorphism is not required (`CA1859`).
   - This avoids virtual or interface call overhead and enables inlining.

3. **Field Scoping & Local Variables (`csharpsquid:S1450`)**
   - Do not declare a private field if it is only used within a single method (such as constructor initialization).
   - Convert private fields used in only one method to local variables (`csharpsquid:S1450`).

4. **Floating-Point Equality Comparisons (`csharpsquid:S1244`)**
   - Never check floating-point equality or inequality with exact values (`== 0f` or `!= 0f`).
   - Use epsilon or absolute tolerance (`MathF.Abs(val) > 1e-6f`).

5. **Utility & Static Classes (`csharpsquid:S1118` & `CA1822`)**
   - Classes that only have static members must be marked `static` (`csharpsquid:S1118`).
   - Mark methods as `static` when they do not access instance data or call instance methods (`CA1822`).

6. **LINQ Simplification for Loops (`csharpsquid:S3267`)**
   - Simplify loops using expressive LINQ methods (`Any()`, `Where()`, `All()`) instead of manual `foreach` + `if` condition checks where appropriate.

7. **Primary Constructors (`IDE0290`)**
   - Prefer C# 12+ Primary Constructors for classes and structs where constructor parameters are assigned to fields or properties (`public class LaunchShipCommand(Spaceship ship, Vector2D launchVelocity) : IGameCommand`).
   - Reference: https://learn.microsoft.com/tr-tr/dotnet/fundamentals/code-analysis/style-rules/ide0290

8. **Modern C# Collection Expressions (`IDE0300`)**
   - Strictly prefer C# collection expressions (`[...]`) over array / collection initializations (`new[] { ... }`, `new List<T> { ... }`).
   - Example: use `SKPathEffect.CreateDash([8f, 6f], 0f)` instead of `SKPathEffect.CreateDash(new[] { 8f, 6f }, 0f)`.
   - Reference: https://learn.microsoft.com/tr-tr/dotnet/fundamentals/code-analysis/style-rules/ide0300

9. **Zero Compiler Warnings Policy**
   - Do not suppress `CS0618` or style rules via pragmas or `.csproj` `<NoWarn>` unless absolutely necessary for SDK package compatibility (like `NU1701`).
   - Every `dotnet build` execution must output precisely:
     ```text
     0 Warning(s)
     0 Error(s)
     ```
