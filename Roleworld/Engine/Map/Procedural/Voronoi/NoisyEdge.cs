using System.Numerics;

namespace Roleworld.Engine.Map.Procedural.Voronoi;

public class NoisyEdge
{
    public Vector2 Corner1;
    public Vector2 Corner2;
    public Vector2 SiteA;
    public Vector2 SiteB;

    public List<Vector2> NoisyPoints = new();
}
