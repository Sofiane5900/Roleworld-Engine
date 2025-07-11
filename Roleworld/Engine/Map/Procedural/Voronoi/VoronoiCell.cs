using System.Numerics;
using SharpVoronoiLib;

namespace Roleworld.Engine.Map.Voronoi;

public class VoronoiCell
{
    public VoronoiSite Site { get; }
    public List<Vector2> Vertices { get; } = new();

    public TerrainType TerrainType { get; set; }

    public VoronoiCell(VoronoiSite site)
    {
        Site = site;
    }
}
