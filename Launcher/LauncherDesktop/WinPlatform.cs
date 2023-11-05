using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Drawing;
using System.Runtime.InteropServices;

namespace Triode.Win
{
    public class WinPlatform : Platform
    {
        IView view;

        public WinPlatform()
        {
            IO = new WinIO(this);
        }

        public override GL CreateGL() => GL.GetApi(
            view ?? throw new Exception("View was not initialized"));

        public override IView CreateView()
            => view = Window.Create(new()
            {
                Title = "Soda game",
                WindowBorder = WindowBorder.Resizable,
                WindowState = WindowState.Normal,
                Size = new Vector2D<int>(480, 800),
                API = new GraphicsAPI(ContextAPI.OpenGL, new(3, 2)),
                VideoMode = VideoMode.Default,
                FramesPerSecond = 60,
                UpdatesPerSecond = 60,
                VSync = true,
                IsVisible = true,
                PreferredDepthBufferBits = 8
            });

        public override void StartGame(IView view)
        {
            try
            {
                new Game.Main.GameCore(this, view);
                
            }
            catch (Exception e)
            {
                Debug("[LAUNCHER] " + e);
                Environment.Exit(e.HResult);
            }
        }

        public override void Debug(object text)
        {
            Console.WriteLine("[GAME] " + text);
        }
    }
}
