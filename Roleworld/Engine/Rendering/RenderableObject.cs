using Silk.NET.OpenGL;

namespace Roleworld.Engine;

public class RenderableObject
{
    private readonly GL _gl;
    private readonly Shader _shader;
    private readonly uint _vao;
    private readonly uint _vertexCount;

    public RenderableObject(
        GL gl,
        Shader shader,
        float[] vertices,
        float[] indices,
        string texturePath
    )
    {
        _gl = gl;
        _shader = shader;

        // 1. Generate and bind VAO
        _vao = _gl.GenVertexArray();
        _gl.BindVertexArray(_vao);
    }
}
