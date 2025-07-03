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

    public void Generate(RectangleF bounds)
    {
        if (Sites.Count == 0)
        {
            return;
        }

        Edges = VoronoiPlane.TessellateOnce(
            Sites,
            bounds.Left,
            bounds.Top,
            bounds.Right,
            bounds.Bottom
        );

        // remove old cells
        Cells.Clear();

        foreach (var site in Sites)
        {
            var cell = new VoronoiCell(new VoronoiSite(site.X, site.Y));

            // get all the vertices of the cell
            var edgesForSite = Edges.Where(e => e.Left == site || e.Right == site).ToList();

            var points = new List<Vector2>();

            foreach (var edge in edgesForSite)
            {
                if (edge.Start != null)
                    points.Add(new Vector2((float)edge.Start.X, (float)edge.Start.Y));
                if (edge.End != null)
                    points.Add(new Vector2((float)edge.End.X, (float)edge.End.Y));
            }

            //  avoid duplicates, keep only dinstinct point
            cell.Vertices.AddRange(points.Distinct());
        }
    }
}
