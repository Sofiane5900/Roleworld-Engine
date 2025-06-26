using Silk.NET.OpenGL;

namespace Roleworld.Engine.Textures;

public class Texture
{
    public uint Id { get; }
    public int Width { get; }
    public int Height { get; }

    public Texture(GL gl, string path) { }
}
