using Silk.NET.OpenGLES;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Sdl.Android;
using Android.Util;
using Android.Graphics;
using Android.Content.Res;
using Java.IO;

namespace Triode.PlAndroid 
{
    public class AndroidPlatform : Platform
    {

        private readonly SilkActivity Activity;

        IView view;

        public AndroidPlatform(SilkActivity silkActivity)
        {
            Activity = silkActivity;
            IO = new AndroidIO(Activity);
        }

        public override GL CreateGL() => GL.GetApi(
            view ?? throw new Exception("View was not initialized"));

        public override IView CreateView() => view = Window.GetView(new()
        {
            API = new GraphicsAPI(ContextAPI.OpenGLES, new(3, 0)),
            FramesPerSecond = 60,
            UpdatesPerSecond = 60,
            VideoMode = VideoMode.Default,
            VSync = true,
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
                view.Close();
            }
        }

        public override void Debug(object text)
        {
            Log.Debug("[GAME] ", text.ToString());
        }
    }
}
