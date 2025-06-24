using System.Drawing;
using Silk.NET.OpenGL;

namespace Roleworld.Engine;

using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.Windowing;

public class MainWindow
{
    private static IWindow _mainWindow;
    private static GL _gl;

    private static VertexArray _vertexArray;
    private static Shader _shader;

    public static void ConstructWindow()
    {
        WindowOptions mainWindowOption = WindowOptions.Default with
        {
            Size = new Vector2D<int>(800, 600),
            Title = "Roleworld Engine",
        };
        _mainWindow = Window.Create(mainWindowOption);

        _mainWindow.Load += OnLoad;
        _mainWindow.Render += OnRender;

        _mainWindow.Run();
    }

    private static unsafe void OnLoad()
    {
        _shader = new Shader(_gl);
        _vertexArray = new VertexArray(_gl);

        _gl = _mainWindow.CreateOpenGL();
        Console.WriteLine("🟢Loading window..");
        _gl.ClearColor(Color.CornflowerBlue);
        _vertexArray = new VertexArray(_gl);
        _vertexArray.DrawVertexBuffer();
    }

    private static void OnUpdate(double deltaTime) { }

    private static void OnRender(double deltaTime)
    {
        _gl.Clear(ClearBufferMask.ColorBufferBit);
        ;
    }
}
