using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi;

public class Cell
{
    public Center Center { get; }
    public List<Vector2> Vertices { get; }
    public TerrainType TerrainType { get; set; }

    public Cell(Center center, List<Vector2> vertices)
    {
        Center = center;
        Vertices = vertices;
    }

    public List<Vector2> GetSortedVertices()
    {
        var center = Center.Position;

        return Vertices.OrderBy(v => MathF.Atan2(v.Y - center.Y, v.X - center.X)).ToList();
    }
}
