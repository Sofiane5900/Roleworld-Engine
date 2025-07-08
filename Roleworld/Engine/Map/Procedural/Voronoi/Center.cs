using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi;

public class Center
{
    public Vector2 Position;
    public bool IsLand;
    public float Elevation;
    public List<Corner> Corners = new();
    public List<Center> Neighbors = new();
}
