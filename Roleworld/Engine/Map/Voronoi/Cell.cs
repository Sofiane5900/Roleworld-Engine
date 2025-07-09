using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi;

public class Cell
{
    public Center Center { get; }
    public List<Vector2> Vertices { get; }
    public TerrainType TerrainType { get; set; }
}
