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

    private static void OnUpdate(double deltaTime)
    {
        float cameraSpeed = 300f * (float)deltaTime;

        if (keyboard.IsKeyPressed(Key.W) || keyboard.IsKeyPressed(Key.Down))
            _camera.Pan(0, -cameraSpeed);
        if (keyboard.IsKeyPressed(Key.S) || keyboard.IsKeyPressed(Key.Up))
            _camera.Pan(0, cameraSpeed);
        if (keyboard.IsKeyPressed(Key.A) || keyboard.IsKeyPressed(Key.Left))
            _camera.Pan(-cameraSpeed, 0);
        if (keyboard.IsKeyPressed(Key.D) || keyboard.IsKeyPressed(Key.Right))
            _camera.Pan(cameraSpeed, 0);
    }
}
