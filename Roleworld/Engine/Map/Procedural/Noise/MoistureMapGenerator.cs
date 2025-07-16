namespace Roleworld.Engine.Map.Procedural.Noise;

public class MoistureMapGenerator
{
    /// <summary>
    /// Generate a moisture matrix normalized ( 0 = very dry, 1 = very humid)
    /// </summary>
    /// <param name="width">Width in pixels </param>
    /// <param name="height">Height in pixels</param>
    /// <param name="seed">Seed for deterministic generation</param>
    /// <returns>float[width,height] containing moisture</returns>
    public static float[,] Generate(int width, int height, int seed)
    {
        var perlin = new PerlinNoise(seed * 7919);
        float[,] map = new float[width, height];
        float scale = 500f;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                map[x, y] = perlin.GenerateNormalizedNoise(x / scale, y / scale);
            }
        }
        return map;
    }
}
