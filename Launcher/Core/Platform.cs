using Silk.NET.Core.Native;
using Silk.NET.Windowing;

namespace Triode;

public abstract class Platform
{
    public IO IO;

    public abstract NativeAPI CreateGL();
    public abstract IView CreateView();
    public abstract void StartGame(IView view);
    public abstract void Debug(object text);

    
}