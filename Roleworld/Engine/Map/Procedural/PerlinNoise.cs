using System.Xml;

namespace Roleworld.Engine.Map;

public class PerlinNoise
{
    public float GeneratePerlin(float x, float y)
    {
        // determine grid cell cordinates
        int x0 = (int)x;
        int y0 = (int)y;
        int x1 = x0 + 1;
        int y1 = y0 + 1;
    }
}
