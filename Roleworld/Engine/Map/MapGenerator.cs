namespace Roleworld.Engine.Map
{
    public class MapGenerator
    {
        private readonly PerlinNoise perlin;

        public MapGenerator()
        {
            perlin = new PerlinNoise();
        }

        public MapData Generate(int width, int height)
        {
            var data = new MapData(width, height);
            float scale = 20f;

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float sampleX = x / scale;
                    float sampleY = y / scale;
                    float noise = perlin.GenerateNormalizedNoise(sampleX, sampleY);
                    Console.WriteLine($"Noise = {noise:0.00}");
                    data.HeightMap[x, y] = noise;
                    data.BiomeMap[x, y] = GetTerrainType(noise);
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
