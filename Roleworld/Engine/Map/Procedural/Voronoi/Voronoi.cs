using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi;

public class Voronoi
{
    public List<Vector2> Sites { get; private set; } = new();

    public Voronoi() { }

    public void AddSite(Vector2 point)
    {
        Sites.Add(point);
    }
}
