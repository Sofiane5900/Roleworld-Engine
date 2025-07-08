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
}
