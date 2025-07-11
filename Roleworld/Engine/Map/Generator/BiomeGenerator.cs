using Roleworld.Engine.Map.Voronoi;

namespace Roleworld.Engine.Map.Generators;

public static class BiomeGenerator
{
    /// <summary>
    /// Sets <c>cell.TerrainType</c> for every Voronoi cell
    /// according to the height at the site position.
    /// </summary>
    public static void Assign(IEnumerable<VoronoiCell> cells, float[,] heightMap)
    {
        int w = heightMap.GetLength(0);
        int h = heightMap.GetLength(1);

        foreach (var cell in cells)
        {
            int x = Math.Clamp((int)cell.Site.X, 0, w - 1);
            int y = Math.Clamp((int)cell.Site.Y, 0, h - 1);

            float elevation = heightMap[x, y];
            cell.TerrainType = ToBiome(elevation);
        }
    }

    private static TerrainType ToBiome(float h) =>
        h switch
        {
            < 0.3f => TerrainType.Water,
            < 0.4f => TerrainType.Sand,
            < 0.6f => TerrainType.Grass,
            < 0.8f => TerrainType.Rock,
            _ => TerrainType.Snow,
        };
}
