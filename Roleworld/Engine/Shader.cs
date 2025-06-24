using Silk.NET.OpenGL;

namespace Roleworld.Engine;

public class Shader
{
    private readonly GL _gl;
    public uint Handle { get; }

    const string vertexCode =
        @"
            #version 330 core

            layout (location = 0) in vec3 aPosition;

            void main()
            {
                gl_Position = vec4(aPosition, 1.0);
            }";

    const string fragmentCode =
        @"
        #version 330 core

        out vec4 out_color;

        void main()
        {
            out_color = vec4(1.0, 0.5, 0.2, 1.0);
        }";

    public Shader(GL gl)
    {
        _gl = gl;
        Handle = _gl.CreateShader(ShaderType.VertexShader);
        _gl.ShaderSource(Handle, vertexCode);
        uint fragmentShader = _gl.CreateShader(ShaderType.FragmentShader);
        _gl.ShaderSource(fragmentShader, fragmentCode);

        _gl.CompileShader(fragmentShader);

        _gl.GetShader(fragmentShader, ShaderParameterName.CompileStatus, out int fStatus);
        if (fStatus != (int)GLEnum.True)
            throw new Exception(
                "Fragment shader failed to compile: " + _gl.GetShaderInfoLog(fragmentShader)
            );
    }

    public void Compile()
    {
        _gl.CompileShader(Handle);
        _gl.GetShader(Handle, ShaderParameterName.CompileStatus, out int vStatus);
        if (vStatus != (int)GLEnum.True)
        {
            throw new Exception("Vertex shader failed to compile: " + _gl.GetShaderInfoLog(Handle));
        }
    }
}
