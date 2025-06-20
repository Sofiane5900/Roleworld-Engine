namespace Roleworld.Engine;
using Silk.NET.Input;
using Silk.NET.Windowing;
using Silk.NET.Maths;

public class MainWindow
{
    private static IWindow _mainWindow;

    public static void ConstructWindow()
    {
        OnLoad();
        
        WindowOptions mainWindowOption = WindowOptions.Default with
        {
            Size = new Vector2D<int>(800,600),
            Title = "Roleworld Engine"
        };
        _mainWindow = Window.Create(mainWindowOption);
        _mainWindow.Run();
    }

    private static void OnLoad()
    {
        Console.WriteLine("🟢Loading window..");
    }

    private static void OnUpdate(double deltaTime) { }

    private static void OnRender(double deltaTime) { }
    
    

}