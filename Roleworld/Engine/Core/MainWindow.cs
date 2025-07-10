using System.Drawing;
using System.Numerics;
using Roleworld.Engine.Map;
using Roleworld.Engine.Rendering;
using Roleworld.Engine.Textures;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Roleworld.Engine.Core;

public class MainWindow
{
    // Window & Rendering
    private static IWindow _mainWindow = null!;
    private static GL _gl = null!;
    private static Shader _shader = null!;

    // Map
    private static MapRenderer _mapRenderer = null!;
    private static MapData _mapData = null!;

    // Camera & Input
    private static Camera2D _camera = null!;
    private static CameraController _cameraController = null!;
    private static IKeyboard _keyboard = null!;

    /// <summary>
    ///  Generate the main window of Roleworld engine
    /// </summary>
    /// <remarks>
    /// This method configure GFLW, OpenGL, and the main loop of our engine
    /// </remarks>
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
        _mainWindow.Update += OnUpdate;

        _mainWindow.Run();
    }

    private static unsafe void OnLoad()
    {
        _gl = _mainWindow.CreateOpenGL();
        _shader = new Shader(_gl);
        _mapData = new MapGenerator(100).Generate(1024, 1024);
        _mapRenderer = new MapRenderer(_gl);
        _camera = new Camera2D();
        var input = _mainWindow.CreateInput();
        _keyboard = input.Keyboards[0];
        _cameraController = new CameraController(_camera, _keyboard);
        _mapRenderer.Build(_mapData);

        Console.WriteLine("Loading main window..");
        _gl.ClearColor(Color.CornflowerBlue);
    }

    private static unsafe void OnRender(double deltaTime)
    {
        _gl.Clear(ClearBufferMask.ColorBufferBit);
        _shader.Use();
        Matrix4x4 projection = _camera.GetProjectionMatrix(
            _mainWindow.FramebufferSize.X,
            _mainWindow.FramebufferSize.Y
        );

        _shader.SetMatrix4("uProjection", projection);

        _mapRenderer.Render();
    }

    private static void OnUpdate(double deltaTime)
    {
        _cameraController.Update(deltaTime);
    }
}
