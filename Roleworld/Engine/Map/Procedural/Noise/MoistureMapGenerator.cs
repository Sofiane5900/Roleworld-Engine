namespace Roleworld.Engine.Map.Procedural.Noise;

public class MoistureMapGenerator
{
    public float[,] Generate(int width, int height, int seed)
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
