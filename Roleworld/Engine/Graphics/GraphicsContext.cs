using System.Drawing;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Roleworld.Engine.Graphics;

public class GraphicsContext
{
    public static GL _gl = null!;
    private static Shader _shader = null!;
    private static IWindow _mainWindow = null!;
    public static GL Gl => _gl;
    public static Shader Shader => _shader;

    public static void Initialize(IWindow window)
    {
        _gl = window.CreateOpenGL();
        _shader = new Shader(_gl);
        _gl.ClearColor(Color.Blue);
    }

    public static void BeginFrame() => Gl.Clear(ClearBufferMask.ColorBufferBit);
}
