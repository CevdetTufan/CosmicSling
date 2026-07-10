# Cosmic Sling (.NET 10 & SkiaSharp) - Workspace Agent Rules

## 1. Avoid Deprecated / Obsolete APIs
- **NEVER** use methods or classes marked with `[Obsolete]` or deprecated in modern `.NET 10` or SkiaSharp (`4.x+`).
- **DO NOT** use `#pragma warning disable CS0618` or `<NoWarn>CS0618</NoWarn>` to hide deprecation warnings.
- Always use the modern replacement API (e.g., non-deprecated `canvas.DrawLine`, `SKFont` instead of obsolete `SKPaint.TextSize`, etc.).

## 2. Architecture & Design Principles
- Respect **Clean Architecture** boundaries (`Domain` -> `Application` -> `Infrastructure` / `Presentation`).
- Maintain **SOLID principles** and proper Gang of Four design patterns (`Strategy`, `Command`, `Observer`, `Factory`, `State`).

## 3. Verification
- Run `dotnet build` and ensure **0 Warning(s), 0 Error(s)** after any code changes.
- Run `dotnet test` to confirm all unit tests pass.
