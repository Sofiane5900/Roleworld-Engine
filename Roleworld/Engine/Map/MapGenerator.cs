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

            // 3. affect terrain type to voronoi cells
            foreach (var cell in voronoi.Cells)
            {
                int cx = (int)cell.Site.X;
                int cy = (int)cell.Site.Y;
                float heightValue = data.HeightMap[
                    Math.Clamp(cx, 0, width - 1),
                    Math.Clamp(cy, 0, height - 1)
                ];
                cell.TerrainType = GetTerrainType(heightValue);
            }

            // 3. Generate noisy edges and apply to cells
            var randNoise = new Random(0); // Deterministic seed for noise
            ApplyNoisyBordersToCells(voronoi.Cells, voronoi.Edges, randNoise);

            // inject voronoi cells in map data to generate them
            data.Cells.Clear();
            data.Cells.AddRange(voronoi.Cells);

            return data;
        }

        /// <summary>
        /// Applies noisy borders to cells by perturbing their vertices directly.
        /// This is a simpler approach that avoids complex edge sorting.
        /// </summary>
        private void ApplyNoisyBordersToCells(
            List<VoronoiCell> cells,
            List<VoronoiEdge> edges,
            Random rng
        )
        {
            var noisyEdgeDict =
                new Dictionary<
                    (System.Numerics.Vector2, System.Numerics.Vector2),
                    List<System.Numerics.Vector2>
                >();
            foreach (var edge in edges)
            {
                if (edge.Left == null || edge.Right == null)
                    continue;
                var start = new System.Numerics.Vector2((float)edge.Start.X, (float)edge.Start.Y);
                var end = new System.Numerics.Vector2((float)edge.End.X, (float)edge.End.Y);
                var noisy = NoisyEdgeGenerator.Generate(edge, 2, 0.05f, rng);
                noisyEdgeDict[(start, end)] = noisy.NoisyPoints;
                noisyEdgeDict[(end, start)] = noisy.NoisyPoints;
            }

            foreach (var cell in cells)
            {
                var originalVertices = new List<System.Numerics.Vector2>(cell.Vertices);
                var newVertices = new List<System.Numerics.Vector2>();
                int n = originalVertices.Count;
                for (int i = 0; i < n; i++)
                {
                    var a = originalVertices[i];
                    var b = originalVertices[(i + 1) % n];
                    if (!noisyEdgeDict.TryGetValue((a, b), out var noisyPoints))
                        if (!noisyEdgeDict.TryGetValue((b, a), out noisyPoints))
                            continue;

                    if (!PointsMatch(noisyPoints[0], a))
                        noisyPoints = noisyPoints.AsEnumerable().Reverse().ToList();

                    for (int j = 0; j < noisyPoints.Count - 1; j++)
                        newVertices.Add(noisyPoints[j]);
                }
                cell.Vertices.Clear();
                cell.Vertices.AddRange(newVertices);
            }
        }

        private bool PointsMatch(
            System.Numerics.Vector2 p1,
            System.Numerics.Vector2 p2,
            float epsilon = 1.0f
        )
        {
            return System.Numerics.Vector2.Distance(p1, p2) < epsilon;
        }

        private TerrainType GetTerrainType(float height)
        {
            return height switch
            {
                < 0.3f => TerrainType.Water,
                < 0.4f => TerrainType.Sand,
                < 0.6f => TerrainType.Grass,
                < 0.8f => TerrainType.Rock,
                _ => TerrainType.Snow,
            };
        }
    }
}
