using Silk.NET.OpenGL;

namespace Roleworld.Engine;

public class VertexArray
{
    private readonly GL _gl;
    private readonly uint _vao;
    public uint Handle { get; }

    public VertexArray(GL gl)
    {
        _gl = gl;
        Handle = gl.GenVertexArray();
        _gl.BindVertexArray(Handle);
    }
}



