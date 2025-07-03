using Silk.NET.Input;

namespace Roleworld.Engine;

public class CameraController
{
    private static Camera2D _camera;
    private static IKeyboard _keyboard;

    public CameraController(Camera2D camera, IKeyboard keyboard)
    {
        _camera = camera;
        _keyboard = keyboard;
    }

    private static void Update(double deltaTime)
    {
        float cameraSpeed = 300f * (float)deltaTime;

        if (_keyboard.IsKeyPressed(Key.W) || _keyboard.IsKeyPressed(Key.Down))
            _camera.Pan(0, -cameraSpeed);
        if (_keyboard.IsKeyPressed(Key.S) || _keyboard.IsKeyPressed(Key.Up))
            _camera.Pan(0, cameraSpeed);
        if (_keyboard.IsKeyPressed(Key.A) || _keyboard.IsKeyPressed(Key.Left))
            _camera.Pan(-cameraSpeed, 0);
        if (_keyboard.IsKeyPressed(Key.D) || _keyboard.IsKeyPressed(Key.Right))
            _camera.Pan(cameraSpeed, 0);
    }
}
