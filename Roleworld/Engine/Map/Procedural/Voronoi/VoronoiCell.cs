using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi;

public class VoronoiCell
{
    public Vector2 Site { get; }
    public List<Vector2> Vertices { get; } = new();
}
