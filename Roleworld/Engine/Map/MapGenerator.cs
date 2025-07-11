using System.Diagnostics;
using System.Drawing;
using Roleworld.Engine.Map.Generators;
using Roleworld.Engine.Map.Procedural;
using Roleworld.Engine.Map.Procedural.Noise;
using Roleworld.Engine.Map.Voronoi;
using SharpVoronoiLib;

namespace Roleworld.Engine.Map
{
    public class MapGenerator
    {
        private readonly PerlinNoise perlin;
        private Voronoi.Voronoi voronoi;

        public MapGenerator(int seed)
        {
            perlin = new PerlinNoise(seed);
        }

        /// <summary>
        /// Generates a new <see cref="MapData"/> instance filled with elevation, biomes,
        /// and Voronoi cells using procedural generation algorithms.
        /// </summary>
        /// <param name="width">Width of the map in tiles.</param>
        /// <param name="height">Height of the map in tiles.</param>
        /// <returns>A fully populated <see cref="MapData"/> instance.</returns>
        public MapData Generate(int width, int height, int seed)
        {
            var data = new MapData(width, height);

            // 1. Perlin & Fallofmap generator
            data.HeightMap = HeightMapGenerator.Generate(width, height, seed);

            // 2. Voronoi generation
            var voronoi = VoronoiGenerator.Generate(width, height, seed, 8000, 5);

            // 3. Affect terrain type to voronoi cells
            BiomeGenerator.Assign(voronoi.Cells, data.HeightMap);

            // 3. Generate noisy edges and apply to cells
            var randNoise = new Random(0); // Deterministic seed for noise
            NoisyEdgeProcessor.ApplyNoisyBordersToCells(voronoi.Cells, voronoi.Edges, randNoise);

            // inject voronoi cells in map data to generate them
            data.Cells.Clear();
            data.Cells.AddRange(voronoi.Cells);

            return data;
        }
    }
}
