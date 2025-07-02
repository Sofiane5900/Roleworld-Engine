using System.Drawing;
using System.Numerics;
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

    private static Camera2D _camera;

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
        _mapData = new MapGenerator(100).Generate(1024, 1024);
        _mapRenderer = new MapRenderer(_gl);
        _camera = new Camera2D();
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
        Matrix4x4 projection = _camera.GetProjectionMatrix(
            _mainWindow.FramebufferSize.X,
            _mainWindow.FramebufferSize.Y
        );

        _shader.SetMatrix4("uProjection", projection);
        _mapRenderer.Render();
    }
}
