namespace Roleworld.Engine.Map;

public static class FallofMapGenerator
{
    private static float Evaluate(float value)
    {
        float a = 3f;
        float b = 2.2f;

        // return a smooth transition from the center of the map to the edge
        return MathF.Pow(value, a) / (MathF.Pow(value, a) + MathF.Pow(b - b * value, a));
    }
}
