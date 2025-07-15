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

    public int ViewportWidth { get; private set; }
    public int ViewportHeight { get; private set; }

    public GraphicsContext(IWindow window)
    {
        _gl = window.CreateOpenGL();
        _shader = new Shader(_gl);

        ViewportWidth = window.FramebufferSize.X;
        ViewportHeight = window.FramebufferSize.Y;
        _gl.ClearColor(Color.DarkGray);
    }

    public void BeginFrame() => Gl.Clear(ClearBufferMask.ColorBufferBit);
}
