using Silk.NET.OpenGL;

namespace Roleworld.Engine;

public class VertexArray
{
    private readonly GL _gl;
    private readonly uint _vao;
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
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _ebo);
    }

    public unsafe void DrawVertexBuffer()
    {
        float[] vertices =
        {
            0.5f,
            0.5f,
            0.0f,
            0.5f,
            -0.5f,
            0.0f,
            -0.5f,
            -0.5f,
            0.0f,
            -0.5f,
            0.5f,
            0.0f,
        };

        fixed (float* buf = vertices)
            _gl.BufferData(
                BufferTargetARB.ArrayBuffer,
                (nuint)(vertices.Length * sizeof(float)),
                buf,
                BufferUsageARB.StaticDraw
            );

        uint[] indices = { 0u, 1u, 3u, 1u, 2u, 3u };

        fixed (uint* buf = indices)
            _gl.BufferData(
                BufferTargetARB.ElementArrayBuffer,
                (nuint)(indices.Length * sizeof(uint)),
                buf,
                BufferUsageARB.StaticDraw
            );
    }
}
