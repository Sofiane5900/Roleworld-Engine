using System.Numerics;

namespace Roleworld.Engine.Map.Procedural.Voronoi;

public class NoisyEdge
{
    public Vector2 Corner1;
    public Vector2 Corner2;
    public Vector2 SiteA;
    public Vector2 SiteB;

    public List<Vector2> NoisyPoints = new();

    public static string GetEdgeId(Vector2 a, Vector2 b)
    {
        if (a.X < b.X || (a.X == b.X && a.Y < b.Y))
        {
            return $"{a.X:F2},{a.Y:F2}-{b.X:F2},{b.Y:F2}";
        }
        else
        {
            return $"{b.X:F2},{b.Y:F2}-{a.X:F2},{a.Y:F2}";
        }
    }
}
