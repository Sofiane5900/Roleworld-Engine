namespace Roleworld.Engine.Map.Procedural.Noise;

/// <summary>
///  Generate 2D Perlin noise based on a given seed.
/// </summary>
public class PerlinNoise
{
    private readonly int _seed;
    private readonly Random _prng;
    private readonly int[] _permutation;

    /// <summary>
    /// Initializes the Perlin Noise generator with a fixed seed.
    /// </summary>
    /// <param name="seed">Seed for deterministic permutation generation</param>
    public PerlinNoise(int seed)
    {
        _seed = seed;
        _prng = new Random(seed);
        var p = new int[256];
        // initialize p array with integer from 0 to 255
        for (int i = 0; i < 256; i++)
        {
            p[i] = i;
        }

        // fisher-yates algorithm to shuffle integers
        for (int i = 255; i > 0; i--)
        {
            int swapIndex = _prng.Next(i + 1);
            int temp = p[i];
            p[i] = p[swapIndex];
            p[swapIndex] = temp;
        }
        // copy 2 time the p array in permutation array to not exceed index
        _permutation = new int[512];
        for (int i = 0; i < 512; i++)
        {
            int index = i % 256;
            _permutation[i] = p[index];
        }
    }

    /// <summary>
    /// List of gradient vectors used to determine noise direction (left, right, bottom, up)
    /// </summary>
    private static readonly float[,] Gradients = new float[,]
    {
        { 1f, 0f }, // right
        { -1f, 0f }, // left
        { 0f, 1f }, // up
        { 0f, -1f }, // down
        { 1f, 1f }, // up-right
        { -1f, 1f }, // up-left
        { 1f, -1f }, //down-right
        { -1f, -1f }, // down-left
    };

    /// <summary>
    /// Computes the gradient vector at a given grid point using the permutation table.
    /// </summary>
    private void GetGradient(int x, int y, out float gx, out float gy)
    {
        int hash = _permutation[(x + _permutation[y & 255]) & 255];
        int index = hash % 8;
        gx = Gradients[index, 0];
        gy = Gradients[index, 1];
    }

    /// <summary>
    /// Computes the dot product between the gradient vector at a grid corner (ix, iy)
    /// and the distance vector from that corner to the point (x, y).
    /// </summary>
    /// <remarks>
    /// This measures how much influence the gradient at the grid corner has
    /// on the final noise value at the point (x, y).
    /// It's used in the interpolation step to generate smooth Perlin noise.
    /// </remarks>
    /// <param name="ix">Grid corner X coordinate</param>
    /// <param name="iy">Grid corner Y coordinate</param>
    /// <param name="x">Point's X coordinate</param>
    /// <param name="y">Point's Y coordinate</param>
    /// <returns>Dot product between gradient and distance vector</returns>
    private float DotGridGradient(int ix, int iy, float x, float y)
    {
        float gx,
            gy;
        GetGradient(ix, iy, out gx, out gy);
        float dx = x - ix;
        float dy = y - iy;
        return dx * gx + dy * gy; // scalar product
    }

    /// <summary>
    /// Smooths the interpolation factor to create seamless transitions between noise values.
    /// </summary>
    /// <param name="t">Interpolation factor between 0 and 1.</param>
    /// <returns>Smoothed interpolation factor.</returns>
    private float Fade(float t)
    {
        return t * t * t * (t * (t * 6 - 15) + 10);
    }

    /// <summary>
    /// Performs a linear interpolation between two values.
    /// </summary>
    /// <param name="a">The start value.</param>
    /// <param name="b">The end value.</param>
    /// <param name="t">Interpolation factor (typically between 0 and 1).</param>
    /// <returns>Interpolated value between <c>a</c> and <c>b</c>.</returns>
    private float Lerp(float a, float b, float t)
    {
        return a + t * (b - a);
    }

    /// <summary>
    /// Generates 2D Perlin noise at the given coordinates (raw value, not normalized).
    /// </summary>
    /// <param name="x">The X coordinate in continuous space.</param>
    /// <param name="y">The Y coordinate in continuous space.</param>
    /// <returns>A float value representing the raw noise (range approximately [-1, 1]).</returns>
    public float GenerateRawNoise(float x, float y)
    {
        int x0 = (int)MathF.Floor(x);
        int x1 = x0 + 1;
        int y0 = (int)MathF.Floor(y);
        int y1 = y0 + 1;

        float sx = Fade(x - x0);
        float sy = Fade(y - y0);

        float n0,
            n1,
            ix0,
            ix1,
            value;

        n0 = DotGridGradient(x0, y0, x, y);
        n1 = DotGridGradient(x1, y0, x, y);
        ix0 = Lerp(n0, n1, sx);

        n0 = DotGridGradient(x0, y1, x, y);
        n1 = DotGridGradient(x1, y1, x, y);
        ix1 = Lerp(n0, n1, sx);

        value = Lerp(ix0, ix1, sy);

        return value;
    }

    /// <summary>
    /// Generates a normalized Perlin noise value in the range [0, 1] at the given 2D coordinates.
    /// </summary>
    /// <param name="x">X coordinate in continuous space.</param>
    /// <param name="y">Y coordinate in continuous space.</param>
    /// <returns>A float value between 0.0 and 1.0.</returns>
    public float GenerateNormalizedNoise(float x, float y)
    {
        return (GenerateRawNoise(x, y) + 1f) / 2f;
    }
}
