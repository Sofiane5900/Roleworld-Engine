using System.Numerics;
using SharpVoronoiLib;

namespace Roleworld.Engine.Map.Voronoi;

/// <summary>
/// Utility class to generate a noisy visual representation of Voronoi edges.
/// This is used to make biome borders and coastlines look more natural.
/// </summary>
public class NoisyEdgeGenerator
{
    /// <summary>
    /// Generates a noisy version of the given Voronoi edge, using midpoint displacement.
    /// </summary>
    /// <param name="edge">The Voronoi edge to transform.</param>
    /// <param name="levels">The number of subdivision levels (2–5 recommended).</param>
    /// <param name="amplitude">The maximum noise displacement at the first level.</param>
    /// <param name="rng">A random number generator (deterministic if needed).</param>
    /// <returns>A NoisyEdge containing the original edge and its noisy render points.</returns>
    public static NoisyEdge Generate(VoronoiEdge edge, int levels, float amplitude, Random rng)
    {
        var points = new List<Vector2>();
        var start = new Vector2((float)edge.Start.X, (float)edge.Start.Y);
        var end = new Vector2((float)edge.End.X, (float)edge.End.Y);

        points.Add(start);

        // Get centers
        Vector2? left = edge.Left is not null
            ? new Vector2((float)edge.Left.X, (float)edge.Left.Y)
            : null;
        Vector2? right = edge.Right is not null
            ? new Vector2((float)edge.Right.X, (float)edge.Right.Y)
            : null;

        // Safety: border edges fallback to center of screen if needed
        var centerA = left ?? right ?? (start + end) / 2;
        var centerB = right ?? left ?? (start + end) / 2;

        Subdivide(points, start, end, centerA, centerB, levels, amplitude, rng);

        return new NoisyEdge(edge, points);
    }

    private static void Subdivide(
        List<Vector2> result,
        Vector2 a,
        Vector2 b,
        Vector2 centerA,
        Vector2 centerB,
        int level,
        float amplitude,
        Random rng
    )
    {
        if (level == 0)
        {
            result.Add(b);
            return;
        }

        float t = 0.3f + (float)rng.NextDouble() * 0.4f; // between 0.3 and 0.7
        Vector2 midCenter = Vector2.Lerp(centerA, centerB, t);
        Vector2 midEdge = Vector2.Lerp(a, b, 0.5f);

        Vector2 dir = Vector2.Normalize(b - a);
        Vector2 perp = new Vector2(-dir.Y, dir.X);
        float offset = ((float)rng.NextDouble() * 2f - 1f) * amplitude;

        Vector2 displaced = Vector2.Lerp(midEdge, midCenter, 0.5f) + perp * offset;

        Subdivide(result, a, displaced, centerA, midCenter, level - 1, amplitude / 2, rng);
        Subdivide(result, displaced, b, midCenter, centerB, level - 1, amplitude / 2, rng);
    }
}
