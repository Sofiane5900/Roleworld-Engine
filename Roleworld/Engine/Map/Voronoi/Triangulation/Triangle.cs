using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi;

public class Triangle
{
    public Vector2 A { get; }
    public Vector2 B { get; }
    public Vector2 C { get; }

    public Triangle(Vector2 a, Vector2 b, Vector2 c)
    {
        A = a;
        B = b;
        C = c;
    }

    public List<Edge2D> GetEdges() =>
        new() { new Edge2D(A, B), new Edge2D(B, C), new Edge2D(C, A) };
}
