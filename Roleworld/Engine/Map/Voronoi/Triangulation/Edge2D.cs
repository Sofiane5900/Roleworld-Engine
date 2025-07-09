using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi;

public readonly struct Edge2D
{
    public Vector2 A { get; }
    public Vector2 B { get; }

    public Edge2D(Vector2 a, Vector2 b)
    {
        A = a;
        B = b;
    }

    public bool IsSameAs(Edge2D other)
    {
        return (A == other.A && B == other.B) || (A == other.B && B == other.A);
    }
}
