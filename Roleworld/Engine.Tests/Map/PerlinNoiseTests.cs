using Roleworld.Engine.Map;
using Xunit;

namespace Engine.Tests.Map;

public class PerlinNoiseTests
{
    [Fact]
    public void Generate_ShouldReturnValueBetween0And1()
    {
        // Arrange
        var perlin = new PerlinNoise();
        float x = 10.5f;
        float y = 7.3f;

        // Act
        float result = perlin.GenerateNormalizedNoise(x, y);

        // Assert
        Assert.InRange(result, 0f, 1f);
    }
}