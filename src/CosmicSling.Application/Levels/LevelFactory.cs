using CosmicSling.Domain.Entities;
using CosmicSling.Domain.Enums;
using CosmicSling.Domain.ValueObjects;

namespace CosmicSling.Application.Levels;

public static class LevelFactory
{
    public static LevelDefinition CreateLevel(int levelIndex)
    {
        return levelIndex switch
        {
            2 => CreateLevel2(),
            3 => CreateLevel3(),
            _ => CreateLevel1()
        };
    }

    private static LevelDefinition CreateLevel1()
    {
        var ship = new Spaceship(1, new Vector2D(150, 400));
        var portal = new Portal(2, new Vector2D(1030, 400));

        var planet = new CelestialBody(
            id: 3,
            position: new Vector2D(600, 400),
            radius: 48f,
            gravityRadius: 360f,
            mass: 14f,
            type: CelestialType.Planet,
            color: GameColor.NeonBlue
        );

        return new LevelDefinition(
            LevelIndex: 1,
            Name: "Level 1: Neon Orbit (Slingshot)",
            Hint: "Gemiyi çekip bırakın. Ortadaki gezegenin kütleçekimini kullanarak portala girin!",
            Ship: ship,
            Portal: portal,
            CelestialBodies: [planet],
			Obstacles: []
		);
    }

    private static LevelDefinition CreateLevel2()
    {
        var ship = new Spaceship(1, new Vector2D(130, 680));
        var portal = new Portal(2, new Vector2D(1050, 150));

        var planetTop = new CelestialBody(
            id: 3,
            position: new Vector2D(520, 240),
            radius: 42f,
            gravityRadius: 330f,
            mass: 12f,
            type: CelestialType.Planet,
            color: GameColor.NeonPurple
        );

        var planetBottom = new CelestialBody(
            id: 4,
            position: new Vector2D(680, 560),
            radius: 42f,
            gravityRadius: 330f,
            mass: 12f,
            type: CelestialType.Planet,
            color: GameColor.NeonBlue
        );

        var obstacle = new Obstacle(5, new Vector2D(600, 400), 24f);

        return new LevelDefinition(
            LevelIndex: 2,
            Name: "Level 2: Twin Gravity Wells",
            Hint: "İki gezegenin kütleçekim koridorundan süzülüp engeli aşın!",
            Ship: ship,
            Portal: portal,
            CelestialBodies: [planetTop, planetBottom],
            Obstacles: [obstacle]
        );
    }

    private static LevelDefinition CreateLevel3()
    {
        var ship = new Spaceship(1, new Vector2D(140, 400));
        var portal = new Portal(2, new Vector2D(1040, 400));

        var repulsor = new CelestialBody(
            id: 3,
            position: new Vector2D(580, 400),
            radius: 40f,
            gravityRadius: 280f,
            mass: 16f,
            type: CelestialType.Repulsor,
            color: GameColor.NeonOrange
        );

        var blackHoleTop = new CelestialBody(
            id: 4,
            position: new Vector2D(580, 150),
            radius: 35f,
            gravityRadius: 300f,
            mass: 11f,
            type: CelestialType.BlackHole,
            color: GameColor.NeonPink
        );

        var blackHoleBottom = new CelestialBody(
            id: 5,
            position: new Vector2D(580, 650),
            radius: 35f,
            gravityRadius: 300f,
            mass: 11f,
            type: CelestialType.BlackHole,
            color: GameColor.NeonPink
        );

        return new LevelDefinition(
            LevelIndex: 3,
            Name: "Level 3: Repulsor Field & Black Holes",
            Hint: "Turuncu alan gemiyi İTER! Pembe kara deliklerin çekimiyle denge kurun.",
            Ship: ship,
            Portal: portal,
            CelestialBodies: [repulsor, blackHoleTop, blackHoleBottom],
            Obstacles: []
        );
    }
}
