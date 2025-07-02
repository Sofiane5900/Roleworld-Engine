using System.Numerics;

namespace Roleworld.Engine;

public class Camera2D
{
    public float X { get; set; } = 0f;
    public float Y { get; set; } = 0f;
    public float Zoom { get; set; } = 1f;

    public float MinZoom { get; set; } = 0.1f;
    public float MaxZoom { get; set; } = 10f;

    public Camera2D() { }

    public Matrix4x4 GetProjectionMatrix(int screenWidth, int screenHeight)
    {
        float viewWidth = screenWidth / Zoom;
        float viewHeight = screenHeight / Zoom;

        return Matrix4x4.CreateOrthographicOffCenter(X, X + viewWidth, Y, Y + viewHeight, -1f, 1f);
    }

    public void Pan(float deltaX, float deltaY)
    {
        X += deltaX / Zoom;
        Y += deltaY / Zoom;
    }

    public void ZoomIn(float factor = 1.1f)
    {
        Zoom *= factor;
        Zoom = MathF.Min(Zoom, MaxZoom);
    }
}
