using SharpVoronoiLib;

namespace Roleworld.Engine.Map.Generators;

public class VoronoiGenerator
{
    /// <summary>
    /// Generate a Voronoi diagram for the different regions of the map
    /// </summary>
    /// <param name="height">height of the map</param>
    /// <param name="width">width of the map</param>
    /// <param name="seed">seed of the map</param>
    /// <param name="nbSites">number of sites to place on the map</param>
    /// <param name="relaxIterations">relax algorithm iterations</param>
    /// <returns>A voronoi diagram instance</returns>
    public static Voronoi.Voronoi Generate(
        int height,
        int width,
        int seed,
        int nbSites,
        int relaxIterations
    )
    {
        Voronoi.Voronoi voronoi = new Voronoi.Voronoi(0, 0, width, height);

        var rand = new Random(seed);
        for (int i = 0; i < nbSites; i++)
        {
            float x = (float)(rand.NextDouble() * width);
            float y = (float)(rand.NextDouble() * height);
            voronoi.AddSite(new VoronoiSite(x, y));
        }

        voronoi.Generate();

        // Lloyd relaxation
        if (relaxIterations > 0)
            voronoi.Relax(relaxIterations, relaxIterations);

        return voronoi;
    }
}
