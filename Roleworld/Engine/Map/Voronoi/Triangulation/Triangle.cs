using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi.Triangulation;

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

    public bool HasVertex(Vector2 v)
    {
        return A == v || B == v || C == v;
    }

    public bool ContainsInCircumcircle(Vector2 p)
    {
        float ax = A.X - p.X;
        float ay = A.Y - p.Y;
        float bx = B.X - p.X;
        float by = B.Y - p.Y;
        float cx = C.X - p.X;
        float cy = C.Y - p.Y;

        float det =
            (ax * ax + ay * ay) * (bx * cy - by * cx)
            - (bx * bx + by * by) * (ax * cy - ay * cx)
            + (cx * cx + cy * cy) * (ax * by - ay * bx);

        return det > 0;
    }
}
