using Silk.NET.Windowing;
using Triode.Game.General;
using Triode.Game.Input;

namespace Triode.Launcher
{
    public abstract class Platform
    {
        public bool IsRunning { get; private set; }

        public IO IO { get; protected set; }
        public readonly InputDevices InputDevices;

        protected Platform()
        {
            IO = NullIO.Instance;
            InputDevices = new InputDevices();
        }

        public void Run()
        {
            if (IsRunning) throw new InvalidOperationException("Platform is already running.");
            IsRunning = true;
            try
            {
                _ = new GameCore(this);
            }
            catch (Exception e)
            {
                Debug("Crashed with an exception: " + e.Message + "\n" + e.StackTrace);
                Environment.Exit(e.HResult);
            }
        }

        public abstract void Debug(object? text);
        public abstract IView CreateView();
    }
}
