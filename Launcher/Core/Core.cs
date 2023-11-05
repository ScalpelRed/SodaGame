using Silk.NET.Windowing;

namespace Triode;

public class Core
{
    public readonly Platform platform;
    public Core(Platform platform) => this.platform = platform;
    IView view;
    public void Run()
    {
        view = platform.CreateView();
        view.Load += () => platform.StartGame(view);
        view.Run();
    }
}