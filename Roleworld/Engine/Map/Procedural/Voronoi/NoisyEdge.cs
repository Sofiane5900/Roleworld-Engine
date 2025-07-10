using System.Numerics;
using SharpVoronoiLib;

namespace Roleworld.Engine.Map.Voronoi;

/// <summary>
///  Data-strcture which aims to contain Voronoi Edges with its noisy render points.
/// </summary>
public class NoisyEdge
{
    public VoronoiEdge Edge;
    public List<Vector2> NoisyPoints;
}
