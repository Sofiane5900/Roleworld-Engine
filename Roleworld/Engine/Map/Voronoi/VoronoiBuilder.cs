using System.Numerics;
using Roleworld.Engine.Map.Voronoi.Triangulation;

namespace Roleworld.Engine.Map.Voronoi;

public class VoronoiBuilder
{
    public VoronoiGraph Build(List<Vector2> points, List<Triangle> triangles)
    {
        var graph = new VoronoiGraph();
        var centerLookup = new Dictionary<Vector2, Center>();
        var cornerLookup = new Dictionary<Vector2, Corner>();

        // 1. create the centers
        foreach (var point in points)
        {
            var center = new Center { Position = point };
            graph.Centers.Add(center);
            centerLookup[point] = center;
        }

        // 2. create the corners
        foreach (var triangle in triangles)
        {
            var centroid = (triangle.A + triangle.B + triangle.C) / 3f;

            if (!cornerLookup.TryGetValue(centroid, out var corner))
            {
                corner = new Corner { Position = centroid };
                graph.Corners.Add(corner);
                cornerLookup[centroid] = corner;
            }

            // for each vertex of triangle (sites) , add a connection
            foreach (var vertex in new[] { triangle.A, triangle.B, triangle.C })
            {
                if (!centerLookup.TryGetValue(vertex, out var center))
                    continue;

                var edge = new Edge { Corner0 = corner, Center0 = center };

                graph.Edges.Add(edge);

                center.Corners.Add(corner);
                corner.Touches.Add(center);
                center.Borders.Add(edge);
                corner.Edges.Add(edge);
            }
        }

        return graph;
    }
}
