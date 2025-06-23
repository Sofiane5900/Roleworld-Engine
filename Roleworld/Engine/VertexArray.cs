using Silk.NET.OpenGL;

namespace Roleworld.Engine;

public class VertexArray
{
    private readonly GL _gl;
    private readonly uint _vao;
    private readonly uint _vbo;
    public uint HandleVAO { get; }

    public VertexArray(GL gl)
    {
        _gl = gl;
        HandleVAO = gl.GenVertexArray();
        _gl.BindVertexArray(HandleVAO);
        _vbo = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, _vbo);
    }

    public unsafe void DrawVertexBuffer()
    {
        float[] vertices =
        {
            0.5f,  0.5f, 0.0f,
            0.5f, -0.5f, 0.0f,
            -0.5f, -0.5f, 0.0f,
            -0.5f,  0.5f, 0.0f
        };    
        
        fixed (float* buf = vertices)
            _gl.BufferData(BufferTargetARB.ArrayBuffer, (nuint) (vertices.Length * sizeof(float)), buf, BufferUsageARB.StaticDraw);
    }
}



