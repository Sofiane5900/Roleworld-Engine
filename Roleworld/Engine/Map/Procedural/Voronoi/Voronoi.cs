using System.Numerics;
using SharpVoronoiLib;

namespace Roleworld.Engine.Map.Voronoi;

public class Voronoi
{
    public List<VoronoiSite> Sites { get; private set; } = new();

    public Voronoi() { }

    public void AddSite(VoronoiSite point)
    {
        Sites.Add(point);
    }

    public void Generate() { }
}
