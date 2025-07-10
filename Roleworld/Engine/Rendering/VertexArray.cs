using Silk.NET.OpenGL;

namespace Roleworld.Engine;

public class VertexArray
{
    private readonly GL _gl;
    private readonly uint _vbo;
    private readonly uint _ebo;
    public uint Handle { get; }

    public VertexArray(GL gl)
    {
        _gl = gl;
        Handle = gl.GenVertexArray();
        _gl.BindVertexArray(Handle);

        // VBO
        _vbo = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);

        // EBO
        _ebo = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);
    }
}
