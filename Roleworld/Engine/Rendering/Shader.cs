using System.Numerics;
using Silk.NET.OpenGL;

namespace Roleworld.Engine;

/// <summary>
/// Encapsulates an OpenGL shader program, used to render graphics on the GPU.
/// </summary>
public class Shader
{
    private readonly GL _gl;
    private static uint _program;
    public uint Handle => _program;

    /// <summary>
    /// Processes each vertex by applying the projection matrix and passing attributes (position, color).
    /// </summary>
    const string vertexCode =
        @"
    #version 330 core
    layout(location = 0) in vec2 aPosition;
    layout(location = 1) in vec3 aColor;

    out vec3 vColor;

    uniform mat4 uProjection;

    void main()
     {
         gl_Position = uProjection * vec4(aPosition, 0.0, 1.0);
         vColor = aColor;
     }
";

    /// <summary>
    /// Assign a final color to each fragment (later to be called pixel) on the screen.
    /// </summary>
    const string fragmentCode =
        @"
    #version 330 core
    in vec3 vColor;
    out vec4 FragColor;

    void main()
    {
        FragColor = vec4(vColor, 1.0);
    }
";

    public Shader(GL gl)
    {
        _gl = gl;

        // Compile Vertex Shader
        uint vertexShader = _gl.CreateShader(ShaderType.VertexShader);
        _gl.ShaderSource(vertexShader, vertexCode);
        _gl.CompileShader(vertexShader);
        _gl.GetShader(vertexShader, ShaderParameterName.CompileStatus, out int vStatus);
        if (vStatus != (int)GLEnum.True)
            throw new Exception("Vertex shader failed: " + _gl.GetShaderInfoLog(vertexShader));

        // Compile Fragment Shader
        uint fragmentShader = _gl.CreateShader(ShaderType.FragmentShader);
        _gl.ShaderSource(fragmentShader, fragmentCode);
        _gl.CompileShader(fragmentShader);
        _gl.GetShader(fragmentShader, ShaderParameterName.CompileStatus, out int fStatus);
        if (fStatus != (int)GLEnum.True)
            throw new Exception("Fragment shader failed: " + _gl.GetShaderInfoLog(fragmentShader));

        // Create Program
        _program = _gl.CreateProgram();
        _gl.AttachShader(_program, vertexShader);
        _gl.AttachShader(_program, fragmentShader);
        _gl.LinkProgram(_program);

        _gl.GetProgram(_program, ProgramPropertyARB.LinkStatus, out int lStatus);
        if (lStatus != (int)GLEnum.True)
            throw new Exception(
                "Shader program failed to link: " + _gl.GetProgramInfoLog(_program)
            );

        // Clean up
        _gl.DetachShader(_program, vertexShader);
        _gl.DetachShader(_program, fragmentShader);
        _gl.DeleteShader(vertexShader);
        _gl.DeleteShader(fragmentShader);
    }

    public void Use() => _gl.UseProgram(_program);

    public unsafe void SetMatrix4(string name, Matrix4x4 matrix)
    {
        int location = _gl.GetUniformLocation(Handle, name);
        if (location == -1)
            Console.WriteLine($"Uniform {name} not found in shader.");

        _gl.UniformMatrix4(location, 1, false, (float*)&matrix);
    }
}
