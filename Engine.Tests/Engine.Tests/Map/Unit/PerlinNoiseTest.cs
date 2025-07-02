using Roleworld.Engine.Map;

namespace Engine.Tests;

public class PerlinNoiseTest
{
    [Fact]
    public void Generate_ShouldReturnValueBetween0And1()
    {
        // Arrange
        var perlin = new PerlinNoise(10);
        float x = 10.5f;
        float y = 7.3f;

        // Act
        float result = perlin.GenerateNormalizedNoise(x, y);

        // Assert
        Assert.InRange(result, 0f, 1f);
    }
    
    [Fact]
    public void Generate_SameCoordinates_ReturnsSameValue()
    {
        var perlin = new PerlinNoise(10);
        float x = 5f;
        float y = 5f;

        float first = perlin.GenerateNormalizedNoise(x, y);
        float second = perlin.GenerateNormalizedNoise(x, y);

        Assert.Equal(first, second);
    }
}