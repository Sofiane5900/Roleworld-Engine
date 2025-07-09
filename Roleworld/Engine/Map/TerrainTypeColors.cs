using System.Numerics;

namespace Roleworld.Engine.Map;

public static class TerrainTypeColors
{
    public static Vector3 GetColor(this TerrainType terrain)
    {
        return terrain switch
        {
            TerrainType.Water => new Vector3(0f, 0.3f, 0.8f),
            TerrainType.Sand => new Vector3(0.9f, 0.8f, 0.4f),
            TerrainType.Grass => new Vector3(0.1f, 0.6f, 0.1f),
            TerrainType.Rock => new Vector3(0.5f, 0.5f, 0.5f),
            TerrainType.Snow => new Vector3(1f, 1f, 1f),
            _ => new Vector3(1f, 0f, 1f), // pink fallback to debug
        };
    }
}
