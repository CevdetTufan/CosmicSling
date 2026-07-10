namespace CosmicSling.Domain.ValueObjects;

public readonly record struct GameColor(byte R, byte G, byte B, byte A = 255)
{
    public static GameColor Cyan => new(0, 255, 255);
    public static GameColor NeonBlue => new(30, 144, 255);
    public static GameColor NeonPink => new(255, 20, 147);
    public static GameColor NeonGreen => new(57, 255, 20);
    public static GameColor NeonOrange => new(255, 140, 0);
    public static GameColor NeonPurple => new(186, 85, 211);
    public static GameColor White => new(255, 255, 255);
    public static GameColor DarkGray => new(40, 44, 52);
}
