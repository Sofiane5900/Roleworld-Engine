namespace Roleworld.Engine.Map
{
    public class MapGenerator
    {
        private readonly Perlin2D perlin;

        public MapGenerator()
        {
            perlin = new Perlin2D();
        }

        public MapData Generate(int width, int height)
        {
            var data = new MapData(width, height);

            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    float noise = perlin.GetValue(x * 0.05f, y * 0.05f); // facteur d’échelle
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
