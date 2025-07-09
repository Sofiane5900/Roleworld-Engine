using System.Numerics;

namespace Roleworld.Engine.Map.Voronoi;

public class Corner
{
    public Vector2 Position;
    public List<Corner> Adjacent = new();
    public List<Center> Touches = new();
}
