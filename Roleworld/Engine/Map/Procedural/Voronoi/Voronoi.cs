using System.Drawing;
using System.Numerics;
using SharpVoronoiLib;

namespace Roleworld.Engine.Map.Voronoi;

public class Voronoi
{
    public List<VoronoiSite> Sites { get; private set; } = new();
    public List<VoronoiCell> Cells { get; private set; } = new();
    public List<VoronoiEdge> Edges { get; private set; } = new();

    public Voronoi() { }

    public void AddSite(VoronoiSite point)
    {
        Sites.Add(point);
    }

    public void Generate(RectangleF bounds) { }
}
