using Silk.NET.OpenGL;

namespace Roleworld.Engine;

public class Shader
{
    private static readonly GL _gl;
    public uint Handle { get; }
    
    const string vertexCode =
        @"
            #version 330 core

            layout (location = 0) in vec3 aPosition;

            void main()
            {
                gl_Position = vec4(aPosition, 1.0);
            }";
    
    const string fragmentCode = @"
        #version 330 core

        out vec4 out_color;

        void main()
        {
            out_color = vec4(1.0, 0.5, 0.2, 1.0);
        }";
    
    
}
