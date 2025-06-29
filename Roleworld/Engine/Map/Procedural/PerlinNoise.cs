namespace Roleworld.Engine.Map;

public class PerlinNoise
{
    // 2D Gradient Vector
    private struct Vector2
    {
        public float X,
            Y;

        public Vector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float Dot(Vector2 other) => X * other.X + Y * other.Y;
    }
}
