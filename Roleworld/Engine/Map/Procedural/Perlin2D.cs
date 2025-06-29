namespace Roleworld.Engine.Map;

// wrapper for perlin algorithm for a 2D usage
public class Perlin2D
{
    private readonly Perlin perlin;
    private readonly float scale;
    private readonly int octaves;
    private readonly double lacunarity;
    private readonly double persistence;

    public Perlin2D(
        float scale = 4f,
        int octaves = 4,
        double lacunarity = 2.0,
        double persistence = 0.5
    )
    {
        this.perlin = new Perlin();
        this.scale = scale;
        this.octaves = octaves;
        this.lacunarity = lacunarity;
        this.persistence = persistence;
    }

    // return a normalized noise between 0 and 1
    public float GetValue(float x, float y)
    {
        double nx = x / scale;
        double ny = y / scale;

        double raw = perlin.NoiseOctaves(
            nx,
            ny,
            z: 0.5,
            numOctaves: octaves,
            lacunarity,
            persistence
        );
        return (float)((raw + 1.0) / 2.0); // normalise [-1,1] -> [0,1]
    }
}
