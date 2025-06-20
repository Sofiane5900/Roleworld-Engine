using Silk.NET.Maths;
using Silk.NET.Input;
using Silk.NET.Windowing;

namespace Roleworld
{
    public class Program
    {
        private static IWindow _mainWindow;
        public static void Main(string[] args)
        {
            WindowOptions mainWindowOption = WindowOptions.Default with
            {
                Size = new Vector2D<int>(800,600),
                Title = "Roleworld Engine"
            };
            _mainWindow = Window.Create(mainWindowOption);
            _mainWindow.Run();
        }  
        
    }
}


