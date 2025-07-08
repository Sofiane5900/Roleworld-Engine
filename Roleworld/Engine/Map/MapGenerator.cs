using System.Diagnostics;
using System.Drawing;
using Roleworld.Engine.Map.Noise;

namespace Roleworld.Engine.Map
{
    public class MapGenerator
    {
        private readonly PerlinNoise perlin;

        // private Voronoi voronoi;

        public MapGenerator(int seed)
        {
            perlin = new PerlinNoise(seed);
        }

        public MapData Generate(int width, int height)
        {
            var data = new MapData(width, height);

            // 1. Perlin and fallof generation
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

            // 2. Voronoi generation
            // voronoi = new Voronoi.Voronoi(0, 0, width, height);

            int nbSites = 4000;
            var rand = new Random();

            for (int i = 0; i < nbSites; i++)
            {
                float x = (float)(rand.NextDouble() * width);
                float y = (float)(rand.NextDouble() * height);
                // voronoi.AddSite(new VoronoiSite(x, y));
            }

            var bounds = new RectangleF(0, 0, width, height);
            // voronoi.Generate();
            // voronoi.Relax(3, 5); // par exemple 3 itérations

            // affect terrain type to voronoi cells
            // foreach (var cell in voronoi.Cells)
            // {
            //     int cx = (int)cell.Site.X;
            //     int cy = (int)cell.Site.Y;
            //     float heightValue = data.HeightMap[
            //         Math.Clamp(cx, 0, width - 1),
            //         Math.Clamp(cy, 0, height - 1)
            //     ];
            //     cell.TerrainType = GetTerrainType(heightValue);
            // }

            // inject voronoi cells in map data to generate them
            // data.Cells.Clear();
            // data.Cells.AddRange(voronoi.Cells);

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
