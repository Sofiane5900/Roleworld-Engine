using Roleworld.Engine.Map.Voronoi;
using Roleworld.Engine.Map.Voronoi;

namespace Roleworld.Engine.Map;

public class MapData
{
    public int Width { get; }
    public int Height { get; }
    public float[,] HeightMap { get; }
    public TerrainType[,] BiomeMap { get; }

    public List<VoronoiCell> Cells { get; } = new();

    // public List<NoisyEdge> NoisyEdges { get; } = new();

    public MapData(int width, int height)
    {
        Width = width;
        Height = height;
        HeightMap = new float[width, height];
        BiomeMap = new TerrainType[width, height];
    }
}
