using System.Numerics;
using SharpVoronoiLib;

namespace Roleworld.Engine.Map.Voronoi;

public class NoisyEdge
{
    public VoronoiEdge Edge;
    public List<Vector2> NoisyPoints;
}
