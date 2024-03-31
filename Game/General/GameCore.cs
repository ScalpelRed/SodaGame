using Silk.NET.Windowing;
using Triode.Game.Graphics;
using Triode.Game.Assets;
using Triode.Launcher;

namespace Triode.Game.General
{
    public class GameCore
    {
        public readonly OpenGL OpenGL;
        public readonly Platform Platform;
        public readonly GameController Controller;
        public readonly AssetManager Assets;
        public readonly Audio.AudioCore Audio;

        public GameCore(Platform platform)
        {
            Platform = platform;
            OpenGL = new OpenGL(this);
            Assets = new AssetManager(this);
            Audio = new Audio.AudioCore(this);
            Controller = new GameController(this);
            OpenGL.Start();
        }

        public void Log(object? message)
        {
            Platform.Debug(message);
        }
    }
}
