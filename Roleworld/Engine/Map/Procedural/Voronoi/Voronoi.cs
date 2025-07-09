using System.Drawing;
using System.Numerics;
using SharpVoronoiLib;

namespace Roleworld.Engine.Map.Voronoi;

public class Voronoi
{
    private VoronoiPlane _plane;
    public List<VoronoiSite> Sites => _plane.Sites;
    public List<VoronoiCell> Cells { get; private set; } = new();
    public List<VoronoiEdge> Edges { get; private set; } = new();
    private List<VoronoiSite> _sites = new();

    public Voronoi(float left, float top, float right, float bottom)
    {
        _plane = new VoronoiPlane(left, top, right, bottom);
    }

    public void AddSite(VoronoiSite site)
    {
        _sites.Add(site);
        _plane.SetSites(_sites);
    }

    public void Generate()
    {
        Edges = _plane.Tessellate();
        UpdateCells();
    }

    public void Relax(int iterations = 1, int strength = 1)
    {
        for (int i = 0; i < iterations; i++)
        {
            Edges = _plane.Relax(strength);
        }
        UpdateCells();
    }

    private void UpdateCells()
    {
        Cells.Clear();
        foreach (var site in Sites)
        {
            var cell = new VoronoiCell(site);
            var sortedPoints = site.ClockwisePoints.Select(p => new Vector2(
                (float)p.X,
                (float)p.Y
            ));
            cell.Vertices.AddRange(sortedPoints);
            Cells.Add(cell);
        }
    }
}
