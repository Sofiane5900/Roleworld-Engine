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
        Assert.Equal(map, map2);
    }
}