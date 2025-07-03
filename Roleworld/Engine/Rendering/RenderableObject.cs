using Silk.NET.OpenGL;

namespace Roleworld.Engine.Rendering;

public class RenderableObject
{
    private readonly GL _gl;
    private readonly Shader _shader;
    private readonly uint _vao;

    public RenderableObject(GL gl, Shader shader, float[] vertices, float[] indices)
    {
        _gl = gl;
        _shader = shader;

        // 1. Generate and bind VAO
        _vao = _gl.GenVertexArray();
        _gl.BindVertexArray(_vao);

        // 2. Generate and fill VBO
        uint vbo = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ArrayBuffer, vbo);
        unsafe
        {
            fixed (float* v = vertices)
            {
                _gl.BufferData(
                    BufferTargetARB.ArrayBuffer,
                    (nuint)(vertices.Length * sizeof(float)),
                    v,
                    BufferUsageARB.StaticDraw
                );
            }
        }

        // 3. Generate and fill EBO
        uint ebo = _gl.GenBuffer();
        _gl.BindBuffer(BufferTargetARB.ElementArrayBuffer, ebo);
        unsafe
        {
            fixed (float* i = indices)
            {
                _gl.BufferData(
                    BufferTargetARB.ElementArrayBuffer,
                    (nuint)(indices.Length * sizeof(float)),
                    i,
                    BufferUsageARB.StaticDraw
                );
            }
        }

        // 4. Vertex Attributes
        const int stride = 5 * sizeof(float); // total size in bytes of a vertex data block

        unsafe
        {
            _gl.EnableVertexAttribArray(0); // aPosition
            _gl.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, stride, (void*)0);

            _gl.EnableVertexAttribArray(1); // aTexCoords
            _gl.VertexAttribPointer(
                1,
                2,
                VertexAttribPointerType.Float,
                false,
                stride,
                (void*)(3 * sizeof(float))
            );

            // 5. Texture
        }
    }
}
