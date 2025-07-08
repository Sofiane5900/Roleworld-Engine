using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi;

public class Edge
{
    public Corner Corner0;
    public Corner Corner1;
    public Center Center0;
    public Center Center1;
    public Vector2 Midpoint => (Corner0.Position + Corner1.Position) / 2f;

    public Edge() { }
}
