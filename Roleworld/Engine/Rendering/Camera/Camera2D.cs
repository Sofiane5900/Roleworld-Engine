using System.Numerics;

namespace Roleworld.Engine;

public class Camera2D
{
    public float X { get; set; }
    public float Y { get; set; }
    public float Zoom { get; set; } = 1.6f;

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

    public void ZoomOut(float factor = 1.1f)
    {
        Zoom /= factor;
        Zoom = MathF.Max(Zoom, MinZoom);
    }

    public void SetPosition(float x, float y)
    {
        X = x;
        Y = y;
    }

    public void CenterOn(float targetX, float targetY, int screenWidth, int screenHeight)
    {
        float viewWidth = screenWidth / Zoom;
        float viewHeight = screenHeight / Zoom;

        X = targetX - viewWidth / 2f;
        Y = targetY - viewHeight / 2f;
    }
}
