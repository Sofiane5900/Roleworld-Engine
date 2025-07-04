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

    private static void Subdivide(List<Vector2> points, Vector2 a, Vector2 b, Random rng, int depth)
    {
        if (depth == 0)
        {
            points.Add(a);
            return;
        }

        Vector2 mid = (a + b) / 2f;
        Vector2 offset = new Vector2(-(b.Y - a.Y), b.X - a.X); // perpendicular
        offset = Vector2.Normalize(offset) * ((float)(rng.NextDouble() - 0.5f) * 5f);

        mid += offset;
        Subdivide(points, a, mid, rng, depth - 1);
        Subdivide(points, mid, b, rng, depth - 1);
    }
}
