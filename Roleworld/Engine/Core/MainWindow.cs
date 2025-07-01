using System.Drawing;
using Roleworld.Engine.Map;
using Roleworld.Engine.Rendering;
using Roleworld.Engine.Textures;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Roleworld.Engine.Core;

public class MainWindow
{
    private static IWindow _mainWindow;
    private static GL _gl;

    // private static VertexArray _vertexArray;
    private static Shader _shader;
    private static MapRenderer _mapRenderer;
    private static MapData _mapData;

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
        _gl = _mainWindow.CreateOpenGL();
        _shader = new Shader(_gl);
        // _vertexArray = new VertexArray(_gl);
        _mapData = new MapGenerator(100).Generate(100, 100);
        _mapRenderer = new MapRenderer(_gl);
        _mapRenderer.Build(_mapData);

        Console.WriteLine("🟢Loading main window..");
        _gl.ClearColor(Color.CornflowerBlue);
        // _vertexArray.DrawVertexBuffer();
    }

    private static void OnUpdate(double deltaTime) { }

    private static unsafe void OnRender(double deltaTime)
    {
        _gl.Clear(ClearBufferMask.ColorBufferBit);
        // _gl.BindVertexArray(_vertexArray.Handle);
        _shader.Use();
        _mapRenderer.Render();
    }
}
