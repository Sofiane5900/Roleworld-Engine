using Roleworld.Engine.Map.Voronoi;

namespace Roleworld.Engine.Map.Generators;

public static class BiomeGenerator
{
    /// <summary>
    /// Tag each Voronoi cell with a biome based on elevation (height map) and
    /// moisture (humidity map). Ocean and coast are treated first.
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

            float elev = heightMap[x, y];
            float moisture = moistureMap[x, y];

            cell.BiomeType = ToBiome(elev, moisture);
        }
    }

    /// <summary>
    /// Biome selection: first water bands, then a 3×3 elevation-vs-moisture grid.
    /// </summary>
    private static Biome ToBiome(float elevation, float moisture)
    {
        const float SeaLevel = 0.30f; // deep ocean below
        const float CoastTop = 0.34f; // shallow / coast water until this height
        const float BeachSand = 0.40f;

        if (elevation < SeaLevel)
            return Biome.DeepWater;
        if (elevation < CoastTop)
            return Biome.CoastWater;
        if (elevation < BeachSand)
            return Biome.Sand;

        int tBand = elevation switch
        {
            < 0.48f => 0, // low-land (warm)
            < 0.72f => 1, // mid-altitude (temperate)
            _ => 2, // high-land (cold)
        };

        int mBand = moisture switch
        {
            < 0.30f => 0, // arid
            < 0.60f => 1, // mesic
            _ => 2, // humid
        };

        return (tBand, mBand) switch
        {
            // Warm band
            (0, 0) => Biome.SubtropicalDesert,
            (0, 1) => Biome.Grassland,
            (0, 2) => Biome.TropicalSeasonalForest,

            // Temperate band
            (1, 0) => Biome.TemperateDesert,
            (1, 1) => Biome.TemperateDeciduousForest,
            (1, 2) => Biome.TemperateRainForest,

            // Cold / high band
            (2, 0) => Biome.Tundra,
            (2, 1) => Biome.Bare,
            (2, 2) => Biome.Snow,

            _ => Biome.Scorched, // scorched fallback
        };
    }
}
