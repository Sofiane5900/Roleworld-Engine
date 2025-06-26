using Silk.NET.OpenGL;
using StbImageSharp;

namespace Roleworld.Engine.Textures;

public class Texture
{
    public uint Id { get; }
    public int Width { get; }
    public int Height { get; }

    public Texture(GL gl, string path)
    {
        using var stream = File.OpenRead(path);
        var image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);

        Id = gl.GenTexture();
    }
}
