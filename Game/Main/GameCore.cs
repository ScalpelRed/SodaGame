using Silk.NET.Windowing;
using Game.Graphics;
using Game.Assets;

namespace Game.Main
{
    public class GameCore
    {
        public readonly OpenGL OpenGL;
        public readonly Triode.Platform Platform;
        public readonly Input Input;
        public readonly GameController Game;
        public readonly AssetManager Assets;
        public readonly Audio.AudioCore Audio;

        public GameCore(Triode.Platform platform, IView view)
        {
            Platform = platform;
            OpenGL = new OpenGL(view, this);
            Assets = new AssetManager(this);
            Audio = new Audio.AudioCore(this);
            Input = new Input(OpenGL);
            Game = new GameController(this);
        }

        public void Log(object? message)
        {
            Platform.Debug(message);
        }
    }
}
