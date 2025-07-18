using SharpVoronoiLib;

namespace Roleworld.Engine.Map.Voronoi;

public class NoisyEdgeProcessor
{
    /// <summary>
    /// Apply the <c>Noisy Edge</c> algorithm to all Voronoi Cells
    /// </summary>
    /// <param name="cells">Complete collection of <see cref="VoronoiCell"/> which forms the diagram</param>
    /// <param name="edges">Edges of the cells</param>
    /// <param name="rng">Random number for the noise</param>
    public static void ApplyNoisyBordersToCells(
        List<VoronoiCell> cells,
        List<VoronoiEdge> edges,
        Random rng
    )
    {
        var noisyEdgeDict =
            new Dictionary<
                (System.Numerics.Vector2, System.Numerics.Vector2),
                List<System.Numerics.Vector2>
            >();
        foreach (var edge in edges)
        {
            if (edge.Left == null || edge.Right == null)
                continue;
            var start = new System.Numerics.Vector2((float)edge.Start.X, (float)edge.Start.Y);
            var end = new System.Numerics.Vector2((float)edge.End.X, (float)edge.End.Y);
            var noisy = NoisyEdgeGenerator.Generate(edge, 2, 0.05f, rng);
            noisyEdgeDict[(start, end)] = noisy.NoisyPoints;
            noisyEdgeDict[(end, start)] = noisy.NoisyPoints;
        }

        foreach (var cell in cells)
        {
            var originalVertices = new List<System.Numerics.Vector2>(cell.Vertices);
            var newVertices = new List<System.Numerics.Vector2>();
            int n = originalVertices.Count;
            for (int i = 0; i < n; i++)
            {
                var a = originalVertices[i];
                var b = originalVertices[(i + 1) % n];
                if (!noisyEdgeDict.TryGetValue((a, b), out var noisyPoints))
                    if (!noisyEdgeDict.TryGetValue((b, a), out noisyPoints))
                        continue;

                if (!PointsMatch(noisyPoints[0], a))
                    noisyPoints = noisyPoints.AsEnumerable().Reverse().ToList();

                for (int j = 0; j < noisyPoints.Count - 1; j++)
                    newVertices.Add(noisyPoints[j]);
            }
            cell.Vertices.Clear();
            cell.Vertices.AddRange(newVertices);
        }
    }

    /// <summary>
    /// Test if two vertices can be considered the same point
    /// </summary>
    /// <param name="p1">First 2D vertices</param>
    /// <param name="p2">Second 2D vertices</param>
    /// <param name="epsilon"></param>
    /// <returns>Maximum tolerance , by default 1 float = 1 pixel in the render</returns>
    private static bool PointsMatch(
        System.Numerics.Vector2 p1,
        System.Numerics.Vector2 p2,
        float epsilon = 1.0f
    )
    {
        return System.Numerics.Vector2.Distance(p1, p2) < epsilon;
    }
}
