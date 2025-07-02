namespace Roleworld.Engine.Map;

public class PerlinNoise
{
    private readonly int _seed;
    private readonly Random _prng;
    private readonly int[] _permutation;

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

    // 2D Gradient Vector


    private static readonly float[,] Gradients = new float[,]
    {
        { 1f, 0f },
        { -1f, 0f },
        { 0f, 1f },
        { 0f, -1f },
        { 1f, 1f },
        { -1f, 1f },
        { 1f, -1f },
        { -1f, -1f },
    };

    private Vector2 GetGradient(int x, int y)
    {
        int hash = _permutation[(x + _permutation[y & 255]) & 255];
        int index = hash % Gradients.Length;
        return Gradients[index];
    }

    private float DotGridGradient(int ix, int iy, float x, float y)
    {
        float gx,
            gy;
        GetGradient(ix, iy, out gx, out gy);
        float dx = x - ix;
        float dy = y - iy;
        return dx * gx + dy * gy; // scalar product
    }

    private float Fade(float t)
    {
        return t * t * t * (t * (t * 6 - 15) + 10);
    }

    private float Lerp(float a, float b, float t)
    {
        return a + t * (b - a);
    }

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

    public float GenerateNormalizedNoise(float x, float y)
    {
        return (GenerateRawNoise(x, y) + 1f) / 2f;
    }
}
