using System.Numerics;
using Roleworld.Engine.Core;
using Roleworld.Engine.Graphics;
using Roleworld.Engine.Rendering;
using Silk.NET.Input;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;

namespace Roleworld.Engine.Map;

public class WorldManager
{
    private static MapRenderer _mapRenderer = null!;
    private static MapData _mapData = null!;
    private GraphicsContext _gfx = null!;
    private GL _gl = null!;
    private IWindow _mainWindow = null!;
    private Camera2D _camera = null!;
    private IKeyboard _keyboard = null!;
    private CameraController _cameraController = null!;

    public WorldManager(GraphicsContext gfx, IWindow window, int width, int height, int seed)
    {
        _gfx = gfx;
        // _gfx.Initialize(_mainWindow);
        _mapData = MapGenerator.Generate(width, height, seed);
        _mapRenderer = new MapRenderer(gfx.Gl);
        _mapRenderer.Build(_mapData);

        _camera = new Camera2D();
        _cameraController = new CameraController(_camera, window.CreateInput().Keyboards[0]);
    }

    public void Update(double deltaTime) => _cameraController.Update(deltaTime);

    public void Draw()
    {
        _gfx.Shader.Use();
        Matrix4x4 projectionMatrix = _camera.GetProjectionMatrix(
            _gfx.ViewportWidth,
            _gfx.ViewportHeight
        );
    }
}
