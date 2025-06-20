using Silk.NET.OpenGL;

namespace Roleworld.Engine;
using Silk.NET.Input;
using Silk.NET.Windowing;
using Silk.NET.Maths;

public class MainWindow
{
    private static IWindow _mainWindow;
    private static GL _gl;

    public static void ConstructWindow()
    {
        
        WindowOptions mainWindowOption = WindowOptions.Default with
        {
            Size = new Vector2D<int>(800,600),
            Title = "Roleworld Engine"
        };
        _mainWindow = Window.Create(mainWindowOption);
        
        _mainWindow.Load += OnLoad;
        
        _mainWindow.Run();
    }

    private static void OnLoad()
    {
        _gl = _mainWindow.CreateOpenGL();
        Console.WriteLine("🟢Loading window..");
    }
    
    

}