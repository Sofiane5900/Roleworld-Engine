namespace Roleworld.Engine.Map;

public static class FallofMapGenerator
{
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

    private static float Evaluate(float value)
    {
        float a = 3f;
        float b = 2.2f;

        // return a smooth transition from the center of the map to the edge
        return MathF.Pow(value, a) / (MathF.Pow(value, a) + MathF.Pow(b - b * value, a));
    }
}
