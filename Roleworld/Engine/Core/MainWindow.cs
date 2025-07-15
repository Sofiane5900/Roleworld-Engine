using System.Drawing;
using System.Numerics;
using Roleworld.Engine.Graphics;
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
        GraphicsContext.Initialize(_mainWindow);
        _mapData = MapGenerator.Generate(width: 1024, height: 1024, seed: 100);
        _mapRenderer = new MapRenderer(GraphicsContext.Gl);
        _camera = new Camera2D();
        var input = _mainWindow.CreateInput();
        _keyboard = input.Keyboards[0];
        _cameraController = new CameraController(_camera, _keyboard);
        _mapRenderer.Build(_mapData);

        Console.WriteLine("Loading main window..");
    }

    private static unsafe void OnRender(double deltaTime)
    {
        GraphicsContext.Gl.Clear(ClearBufferMask.ColorBufferBit);
        GraphicsContext.Shader.Use();
        Matrix4x4 projection = _camera.GetProjectionMatrix(
            _mainWindow.FramebufferSize.X,
            _mainWindow.FramebufferSize.Y
        );

        GraphicsContext.Shader.SetMatrix4("uProjection", projection);

        _mapRenderer.Render();
    }

    private static void OnUpdate(double deltaTime)
    {
        _cameraController.Update(deltaTime);
    }
}
