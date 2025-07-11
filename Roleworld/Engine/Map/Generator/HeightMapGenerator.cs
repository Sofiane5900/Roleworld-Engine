using Roleworld.Engine.Map.Procedural.Noise;

namespace Roleworld.Engine.Map.Generators;

public static class HeightMapGenerator
{
    /// <summary>
    /// Generate a normalized [0,1] matrix combining
    /// Perlin noise and a Fallof ma.
    /// </summary>
    /// <param name="width">width of the map</param>
    /// <param name="height">height of the map</param>
    /// <param name="seed">seed of the map</param>
    /// <returns>A float matrix with datas of elevation</returns>
    public static float[,] Generate(int width, int height, int seed = 0)
    {
        var perlin = new PerlinNoise(seed);
        var falloff = Procedural.Noise.FallofMapGenerator.Generate(width, height);
        float[,] map = new float[width, height];
        float scale = 150f;

        for (int x = 0; x < width; x++)
        for (int y = 0; y < height; y++)
        {
            float noise = perlin.GenerateNormalizedNoise(x / scale, y / scale);
            map[x, y] = Math.Clamp(noise - falloff[x, y], 0f, 1f);
        }
        return map;
    }
}
