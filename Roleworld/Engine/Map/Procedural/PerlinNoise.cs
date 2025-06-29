namespace Roleworld.Engine.Map;

public class PerlinNoise
{
    // 2D Gradient Vector
    private struct Vector2
    {
        public float X,
            Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float Dot(Vector2 other) => X * other.X + Y * other.Y;
    }

    private static readonly Vector2[] Gradients = new[]
    {
        new Vector2(1, 0),
        new Vector2(-1, 0),
        new Vector2(0, 1),
        new Vector2(0, -1),
        new Vector2(1, 1),
        new Vector2(-1, 1),
        new Vector2(1, -1),
        new Vector2(-1, -1),
    };

    private Vector2 GetGradient(int x, int y)
    {
        int hash = (x * 73856093) ^ (y * 19349663);
        int index = Math.Abs(hash) % Gradients.Length;
        return Gradients[index];
    }

    private float DotGridGradient(int ix, int iy, float x, float y)
    {
        var gradient = GetGradient(ix, iy);
        float dx = x - ix;
        float dy = y - iy;

        return gradient.Dot(new Vector2(dx, dy));
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

    public float Generate(float x, float y)
    {
        return (GeneratePerlin(x, y) + 1f) / 2f;
    }
}
