using System.Diagnostics;

namespace Roleworld.Engine.Map
{
    public class MapGenerator
    {
        private readonly PerlinNoise perlin;

        public MapGenerator(int seed)
        {
            perlin = new PerlinNoise(seed);
        }

        public MapData Generate(int width, int height)
        {
            var data = new MapData(width, height);
            float[,] falloffMap = FallofMapGenerator.Generate(width, height);
            float scale = 150f;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float sampleX = x / scale;
                    float sampleY = y / scale;

                    float perlinValue = perlin.GenerateNormalizedNoise(sampleX, sampleY);
                    float falloffValue = falloffMap[x, y];

                    float heightValue = Math.Clamp(perlinValue - falloffValue, 0f, 1f);
                    data.HeightMap[x, y] = heightValue;
                    data.BiomeMap[x, y] = GetTerrainType(heightValue);
                }
            }
            return data;
        }

        private TerrainType GetTerrainType(float height)
        {
            if (height < 0.3f)
                return TerrainType.Water;
            if (height < 0.4f)
                return TerrainType.Sand;
            if (height < 0.6f)
                return TerrainType.Grass;
            if (height < 0.8f)
                return TerrainType.Rock;
            return TerrainType.Snow;
        }
    }
}
