using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi;

public class Edge
{
    public Corner Corner0;
    public Corner Corner1;
    public Center Center0;
    public Center Center1;
    public Vector2 Midpoint => (Corner0.Position + Corner1.Position) / 2f;

    public Edge(Corner corner0, Corner corner1, Center center0, Center center1)
    {
        Corner0 = corner0;
        Corner1 = corner1;
        Center0 = center0;
        Center1 = center1;
    }
}
