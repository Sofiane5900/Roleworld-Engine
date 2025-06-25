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
        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, _ebo);
    }

    public unsafe void DrawVertexBuffer()
    {
        float[] vertices =
        {
            //       aPosition     | aTexCoords
            0.5f,
            0.5f,
            0.0f,
            1.0f,
            1.0f,
            0.5f,
            -0.5f,
            0.0f,
            1.0f,
            0.0f,
            -0.5f,
            -0.5f,
            0.0f,
            0.0f,
            0.0f,
            -0.5f,
            0.5f,
            0.0f,
            0.0f,
            1.0f,
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

        const uint positionLoc = 0;
        _gl.EnableVertexAttribArray(positionLoc);
        // 3 floats for position + 2 floats for texture coordinates! \/
        _gl.VertexAttribPointer(
            positionLoc,
            3,
            VertexAttribPointerType.Float,
            false,
            5 * sizeof(float),
            (void*)0
        );

        const uint texCoordLoc = 1;
        _gl.EnableVertexAttribArray(texCoordLoc);
        _gl.VertexAttribPointer(
            texCoordLoc,
            2,
            VertexAttribPointerType.Float,
            false,
            5 * sizeof(float),
            (void*)(3 * sizeof(float))
        );

        _gl.BindVertexArray(0);
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, 0);
    }
}
