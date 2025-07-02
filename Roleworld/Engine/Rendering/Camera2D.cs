namespace Roleworld.Engine;

public class Camera2D
{
    public float X { get; set; } = 0f;
    public float Y { get; set; } = 0f;
    public float Zoom { get; set; } = 1f;

    public float MinZoom { get; set; } = 0.1f;
    public float MaxZoom { get; set; } = 10f;
}
