using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi;

public class PointGenerator
{
    public static List<Vector2> GenerateJitteredGrid(
        int width,
        int height,
        int numCellsX,
        int numCellsY,
        float jitterAmount = 0.4f
    )
    {
        List<Vector2> points = new();
        float cellWidth = (float)width / numCellsX;
        float cellHeight = (float)height / numCellsY;

        Random rng = new();

        for (int x = 0; x < numCellsX; x++)
        {
            for (int y = 0; y < numCellsY; y++)
            {
                float jitterX = (float)(rng.NextDouble() * 2 - 1) * jitterAmount * cellWidth;
                float jitterY = (float)(rng.NextDouble() * 2 - 1) * jitterAmount * cellHeight;

                float px = (x + 0.5f) * cellWidth + jitterX;
                float py = (y + 0.5f) * cellHeight + jitterY;

                points.Add(new Vector2(px, py));
            }
        }

        return points;
    }
}
