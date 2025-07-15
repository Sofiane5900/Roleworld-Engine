using System.Drawing;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Roleworld.Engine.Graphics;

public class GraphicsContext
{
    public GL _gl = null!;
    private Shader _shader = null!;
    private IWindow _mainWindow = null!;
    public GL Gl => _gl;
    public Shader Shader => _shader;

    public void Initialize(IWindow window)
    {
        _gl = window.CreateOpenGL();
        _shader = new Shader(_gl);
        _gl.ClearColor(Color.Blue);
    }

    public void BeginFrame() => Gl.Clear(ClearBufferMask.ColorBufferBit);
}
