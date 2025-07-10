namespace Roleworld.Engine.Map.Procedural.Noise;

public static class FallofMapGenerator
{
    /// <summary>
    /// Generates a 2D Falloff Map that smoothly transitions from the center (value near 0)
    /// to the edges (value near 1). This is often used to simulate an island shape by reducing
    /// terrain elevation near the borders of the map.
    /// </summary>
    /// <param name="width">Width of the map in pixels or grid units.</param>
    /// <param name="height">Height of the map in pixels or grid units.</param>
    /// <returns>A 2D array of floats with values between 0 and 1 representing the falloff gradient.</returns>
    public static float[,] Generate(int width, int height)
    {
        float[,] map = new float[width, height];

        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                float nx = x / (float)width * 2f - 1f;
                float ny = y / (float)height * 2f - 1f;

                float value = MathF.Max(MathF.Abs(nx), MathF.Abs(ny));
                map[x, y] = Evaluate(value);
            }
        }
        // return a normalized 2D array layer for the final map
        return map;
    }

    /// <summary>
    /// Applies a smooth mathematical function to control the steepness of the falloff.
    /// Produces a value between 0 and 1 that increases rapidly near the edges and remains low near the center.
    /// </summary>
    /// <param name="value">A normalized distance value between 0 (center) and 1 (edge).</param>
    /// <returns>A float between 0 and 1 representing the falloff strength at this point.</returns>
    private static float Evaluate(float value)
    {
        float a = 3f;
        float b = 2.2f;

        // return a smooth transition from the center of the map to the edge
        return MathF.Pow(value, a) / (MathF.Pow(value, a) + MathF.Pow(b - b * value, a));
    }
}
