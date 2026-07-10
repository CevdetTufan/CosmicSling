# Cosmic Sling (.NET 10 & SkiaSharp) - Workspace Agent Rules

## 1. Avoid Deprecated / Obsolete APIs
- **NEVER** use methods or classes marked with `[Obsolete]` or deprecated in modern `.NET 10` or SkiaSharp (`4.x+`).
- **DO NOT** use `#pragma warning disable CS0618` or `<NoWarn>CS0618</NoWarn>` to hide deprecation warnings.
- Always use the modern replacement API (e.g., non-deprecated `canvas.DrawLine`, `SKFont` instead of obsolete `SKPaint.TextSize`, etc.).

## 2. Modern C# Style Rules & Code Analysis (`IDE0290`, `IDE0300`, `CA1822`, `CA1859`, `S1118`, `S1244`, `S1450`, `S3267`)
- **Concrete Types for Performance (`CA1859`):** Use concrete types instead of interfaces for private fields/locals to avoid virtual call overhead and enable inlining.
- **Field Scoping (`S1450`):** Do not declare a private field if it is only used within a single method. Declare it as a local variable instead.
- **Floating-Point Equality (`S1244`):** Avoid exact `==` or `!=` comparisons on floating-point numbers (`float`, `double`). Use tolerance ranges (`MathF.Abs(val) > 1e-6f`).
- **Static Utility Classes (`S1118`):** Mark classes containing only static members as `static`.
- **LINQ Simplification (`S3267`):** Simplify manual `foreach` + `if` loops using LINQ (`Any()`, `Where()`).
- **Static Members (`CA1822`):** Mark methods as `static` when they do not access instance data or call instance methods.
- **Primary Constructors (`IDE0290`):** Use C# 12+ primary constructor syntax (`public class MyClass(Param p)`) when appropriate.
  - Reference: https://learn.microsoft.com/tr-tr/dotnet/fundamentals/code-analysis/style-rules/ide0290
- **Collection Expressions (`IDE0300`):** Prefer modern collection expressions (`[...]`) instead of explicit `new[] { ... }` or `new List<T> { ... }`.
  - Reference: https://learn.microsoft.com/tr-tr/dotnet/fundamentals/code-analysis/style-rules/ide0300

## 3. Architecture & Design Principles
- Respect **Clean Architecture** boundaries (`Domain` -> `Application` -> `Infrastructure` / `Presentation`).
- Maintain **SOLID principles** and proper Gang of Four design patterns (`Strategy`, `Command`, `Observer`, `Factory`, `State`).

## 4. Verification
- Run `dotnet build` and ensure **0 Warning(s), 0 Error(s)** after any code changes.
- Run `dotnet test` to confirm all unit tests pass.
