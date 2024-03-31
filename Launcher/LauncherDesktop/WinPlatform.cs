using Silk.NET.Maths;
using Silk.NET.Windowing;

namespace Triode.Launcher.Win
{
    public class WinPlatform : Platform
    {
        public WinPlatform() : base()
        {
            IO = new WinIO(this);
        }

        public override IView CreateView()
        {
            return Window.Create(new()
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
                Samples = 4,
                PreferredDepthBufferBits = 8,
                PreferredStencilBufferBits = 8,
            });
        }

        public override void Debug(object text)
        {
            Console.WriteLine("[GAME] " + text);
        }


        /*IView view;

        public WinPlatform()
        {
            
        }

        public override GL CreateGL() => GL.GetApi(
            view ?? throw new Exception("View was not initialized"));

        public override IView CreateView()
            => 

        public override void StartGame(IView view)
        {
            
        }

        public override void Debug(object text)
        {
            Console.WriteLine("[GAME] " + text);
        }*/
    }
}
