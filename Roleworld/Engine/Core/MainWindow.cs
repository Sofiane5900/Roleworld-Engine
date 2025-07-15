using System.Numerics;
using Roleworld.Engine.Graphics;
using Roleworld.Engine.Map;
using Roleworld.Engine.Rendering;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Roleworld.Engine.Core;

public class MainWindow
{
    // Window & Rendering
    private static IWindow _mainWindow = null!;

    // Camera & Input
    private static Camera2D _camera = null!;
    private static CameraController _cameraController = null!;
    private static IKeyboard _keyboard = null!;

    private static GraphicsContext _gfx;
    private static WorldManager _world;

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
        _gfx = new GraphicsContext();
        _world = new WorldManager(_gfx, _mainWindow, 1024, 1024, 100);
        Console.WriteLine("Loading main window..");
    }

    private static unsafe void OnRender(double deltaTime)
    {
        _gfx.BeginFrame();

        GraphicsContext.Shader.Use();
        Matrix4x4 projection = _camera.GetProjectionMatrix(
            _mainWindow.FramebufferSize.X,
            _mainWindow.FramebufferSize.Y
        );

        GraphicsContext.Shader.SetMatrix4("uProjection", projection);
    }

    private static void OnUpdate(double deltaTime)
    {
        _cameraController.Update(deltaTime);
    }
}
