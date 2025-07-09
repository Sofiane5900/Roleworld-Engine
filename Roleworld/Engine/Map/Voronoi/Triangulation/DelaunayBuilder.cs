using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi.Triangulation;

public class DelaunayBuilder
{
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
}
