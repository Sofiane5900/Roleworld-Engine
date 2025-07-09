using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi.Triangulation;

public class DelaunayBuilder
{
    public static List<Triangle> Triangulate(List<Vector2> points)
    {
        List<Triangle> triangles = new();

        // 1. create super triangle
        Triangle superTriangle = CreateSuperTriangle(points);
        triangles.Add(superTriangle);

        foreach (var point in points)
        {
            // 2. find all triangles violating deulaunay rule
            var badTriangles = triangles.Where(t => t.ContainsInCircumcircle(point)).ToList();

            // 3. find the edge of the hole caused by the bad triangles
            List<Edge2D> polygon = FindBoundary(badTriangles);

            // 4. delete the affected bad triangles
            triangles.RemoveAll(t => badTriangles.Contains(t));

            // 5. re-triangulate the hole
            foreach (var edge in polygon)
            {
                triangles.Add(new Triangle(edge.A, edge.B, point));
            }
        }

        // 6. remove all triangles with a vertex of the super triangle
        triangles.RemoveAll(t =>
            t.HasVertex(superTriangle.A)
            || t.HasVertex(superTriangle.B)
            || t.HasVertex(superTriangle.C)
        );

        return triangles;
    }

    private static Triangle CreateSuperTriangle(List<Vector2> points)
    {
        float minX = float.MaxValue;
        float minY = float.MaxValue;
        float maxX = float.MinValue;
        float maxY = float.MinValue;

        foreach (var point in points)
        {
            if (point.X < minX)
                minX = point.X;
            if (point.Y < minY)
                minY = point.Y;
            if (point.X > maxX)
                maxX = point.X;
            if (point.Y > maxY)
                maxY = point.Y;
        }

        float dx = maxX - minX;
        float dy = maxY - minY;
        float deltaMax = MathF.Max(dx, dy);
        float midX = (minX + maxX) / 2f;
        float midY = (minY + maxY) / 2f;

        // create a gigantic triangle based on the points
        Vector2 p1 = new Vector2(midX - 20 * deltaMax, midY - deltaMax);
        Vector2 p2 = new Vector2(midX, midY + 20 * deltaMax);
        Vector2 p3 = new Vector2(midX + 20 * deltaMax, midY - deltaMax);

        return new Triangle(p1, p2, p3);
    }

    private static List<Edge2D> FindBoundary(List<Triangle> badTriangles)
    {
        Dictionary<Edge2D, int> edgeCount = new();

        foreach (var triangle in badTriangles)
        {
            foreach (var edge in triangle.GetEdges())
            {
                bool found = false;

                foreach (var key in edgeCount.Keys)
                {
                    if (edge.IsSameAs(key))
                    {
                        edgeCount[key]++;
                        found = true;
                        break;
                    }
                }

                if (!found)
                {
                    edgeCount[edge] = 1;
                }
            }
        }

        // only keep triangles which edges are unique
        return edgeCount.Where(kvp => kvp.Value == 1).Select(kvp => kvp.Key).ToList();
    }
}
