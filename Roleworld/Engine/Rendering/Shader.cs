using Silk.NET.OpenGL;

namespace Roleworld.Engine;

public class Shader
{
    private readonly GL _gl;
    private static uint _program;
    public uint Handle => _program;

    const string vertexCode =
        @"
                    #version 330 core
                    layout (location = 0) in vec3 aPosition;
                    // Add a new input attribute for the texture coordinates
                    layout (location = 1) in vec2 aTextureCoord;

                    // Add an output variable to pass the texture coordinate to the fragment shader
                    // This variable stores the data that we want to be received by the fragment
                    out vec2 frag_texCoords;

                    void main()
                    {
                        gl_Position = vec4(aPosition, 1.0);
                        // Assigin the texture coordinates without any modification to be recived in the fragment
                        frag_texCoords = aTextureCoord;
                    }";

    const string fragmentCode =
        @"
                      #version 330 core

                in vec2 frag_texCoords;

                out vec4 out_color;

                uniform sampler2D uTexture;

                void main()
                {
                    out_color = texture(uTexture, frag_texCoords);
                }";

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
}
