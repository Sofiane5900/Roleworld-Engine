using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi;

/// <summary>
/// Utility class to generate a noisy visual representation of Voronoi edges.
/// This is used to make biome borders and coastlines look more natural.
/// </summary>
public class NoisyEdgeGenerator
{
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
