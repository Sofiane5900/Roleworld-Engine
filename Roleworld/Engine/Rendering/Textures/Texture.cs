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
        gl.BindTexture(TextureTarget.Texture2D, Id);

        gl.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureWrapS,
            (int)GLEnum.Repeat
        );
        gl.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureWrapT,
            (int)GLEnum.Repeat
        );
        gl.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureMinFilter,
            (int)GLEnum.Linear
        );
        gl.TexParameter(
            TextureTarget.Texture2D,
            TextureParameterName.TextureMagFilter,
            (int)GLEnum.Linear
        );
    }
}
