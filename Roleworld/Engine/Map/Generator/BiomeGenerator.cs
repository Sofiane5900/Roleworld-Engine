using Roleworld.Engine.Map.Voronoi;

namespace Roleworld.Engine.Map.Generators;

public static class BiomeGenerator
{
    /// <summary>
    /// Sets <c>cell.TerrainType</c> for every Voronoi cell
    /// according to the height at the site position.
    /// </summary>
    public static void Assign(
        IEnumerable<VoronoiCell> cells,
        float[,] heightMap,
        float[,] moistureMap
    )
    {
        int w = heightMap.GetLength(0);
        int h = heightMap.GetLength(1);

        foreach (var cell in cells)
        {
            int x = Math.Clamp((int)cell.Site.X, 0, w - 1);
            int y = Math.Clamp((int)cell.Site.Y, 0, h - 1);

            float elevation = heightMap[x, y];
            float moisture = moistureMap[x, y];

            cell.BiomeType = ToBiome(elevation, moisture);
        }
    }

    private static Biome ToBiome(float elevation, float moisture)
    {
        const float SeaLevel = 0.30f; // below is ocean
        const float BeachTop = 0.33f; // coast

        if (elevation < SeaLevel)
        {
            return Biome.Water;
        }

        if (elevation < BeachTop)
        {
            return Biome.Sand;
        }

        int tBand = elevation switch
        {
            < 0.45f => 0,
            < 0.70f => 1,
            _ => 2,
        };

        int mBand = moisture switch
        {
            < 0.30f => 0,
            < 0.60f => 1,
            _ => 2,
        };

        return (tBand, mBand) switch
        {
            // hot plains
            (0, 0) => Biome.SubtropicalDesert,
            (0, 1) => Biome.Grassland,
            (0, 2) => Biome.TropicalSeasonalForest,

            // temperate hills
            (1, 0) => Biome.TemperateDesert,
            (1, 1) => Biome.TemperateDeciduousForest,
            (1, 2) => Biome.TemperateRainForest,

            // cold mountain
            (2, 0) => Biome.Tundra,
            (2, 1) => Biome.Bare,
            (2, 2) => Biome.Snow,

            // fallback to scorched
            _ => Biome.Scorched,
        };
    }
}
