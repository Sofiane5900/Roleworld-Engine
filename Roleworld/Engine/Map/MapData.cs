using Roleworld.Engine.Map.Voronoi;
using Roleworld.Engine.Map.Voronoi;

namespace Roleworld.Engine.Map;

/// <summary>
///  Represents the data structure to of our map
/// Store diemnsion, elevation, biome data, and Voronoi regions (cells)
/// </summary>
public class MapData
{
    public int Width { get; }
    public int Height { get; }
    public float[,] HeightMap { get; set; }
    public Biome[,] BiomeMap { get; set; }
    public float[,] MoistureMap { get; set; }
    public List<VoronoiCell> Cells { get; } = new();

    public MapData(int width, int height)
    {
        Width = width;
        Height = height;
        HeightMap = new float[width, height];
        BiomeMap = new Biome[width, height];
        MoistureMap = new float[width, height];
    }
}
