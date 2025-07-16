using System.Numerics;

namespace Roleworld.Engine.Map;

/// <summary>
/// Default color for every biome (Whittaker diagram).
/// Values are 0–1 RGB
/// </summary>
public static class BiomeTypeColors
{
    public static Vector3 GetColor(this Biome biome) =>
        biome switch
        {
            Biome.DeepWater => new Vector3(0.02f, 0.22f, 0.55f), // dark open-sea blue
            Biome.CoastWater => new Vector3(0.04f, 0.36f, 0.75f), // lighter coastal blue
            Biome.Sand => new Vector3(0.93f, 0.86f, 0.55f), // pale beach / dune

            // Polar / high-altitude biomes
            Biome.Snow => new(0.96f, 0.96f, 0.96f), // pure white
            Biome.Tundra => new(0.78f, 0.84f, 0.78f), // pale grey-green
            Biome.Bare => new(0.70f, 0.70f, 0.70f), // light rock
            Biome.Scorched => new(0.45f, 0.45f, 0.45f), // dark rock

            // Boreal / sub-alpine forests
            Biome.Taiga => new(0.34f, 0.52f, 0.34f), // spruce green

            // Temperate biomes
            Biome.Shrubland => new(0.50f, 0.60f, 0.40f), // sage/olive
            Biome.TemperateDesert => new(0.84f, 0.80f, 0.52f), // beige steppe
            Biome.TemperateRainForest => new(0.23f, 0.47f, 0.25f), // dark moss
            Biome.TemperateDeciduousForest => new(0.29f, 0.62f, 0.29f), // medium green

            // Grasslands & savannas
            Biome.Grassland => new(0.60f, 0.78f, 0.38f), // prairie green

            // Tropical biomes
            Biome.TropicalRainForest => new(0.15f, 0.55f, 0.20f), // deep jungle
            Biome.TropicalSeasonalForest => new(0.22f, 0.65f, 0.25f), // bright jungle

            // Hot deserts
            Biome.SubtropicalDesert => new(0.93f, 0.85f, 0.55f), // hot-sand beige

            // Debug fallback (magenta)
            _ => new(1.0f, 0.0f, 1.0f),
        };
}
