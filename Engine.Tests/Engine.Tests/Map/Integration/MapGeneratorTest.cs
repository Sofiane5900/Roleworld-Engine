using Roleworld.Engine.Map;

namespace Engine.Tests.Map.Integration;

public class MapGeneratorTest
{
    [Fact]
    public void GenerateMap_WithSeed_ReturnsConsistentAndValidMap()
    {
        int seed = 42;
        var mapGen = new MapGenerator(seed);

        var map = mapGen.Generate(256, 256);

        Assert.NotNull(map);
        Assert.Equal(256, map.Width);
        Assert.Equal(256, map.Height);

        var map2 = mapGen.Generate(256, 256);
        for (int x = 0; x < map.Width; x++)
        {
            for (int y = 0; y < map.Height; y++)
            {
                Assert.Equal(map.HeightMap[x, y], map2.HeightMap[x, y]);
                Assert.Equal(map.BiomeMap[x, y], map2.BiomeMap[x, y]);
            }
        }
    }
}
