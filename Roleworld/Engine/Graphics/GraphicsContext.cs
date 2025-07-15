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

    public uint ViewportWidth { get; private set; }
    public uint ViewportHeight { get; private set; }

    public GraphicsContext(IWindow window) { }

    public void BeginFrame() => Gl.Clear(ClearBufferMask.ColorBufferBit);
}
