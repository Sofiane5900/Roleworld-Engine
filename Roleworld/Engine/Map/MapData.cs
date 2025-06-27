namespace Roleworld.Engine.Map;

public class MapData
{
    public int Width { get; }
    public int Height { get; }
    public float[,] HeightMap { get; }
    public Biome[,] BiomeMap { get; }
}
