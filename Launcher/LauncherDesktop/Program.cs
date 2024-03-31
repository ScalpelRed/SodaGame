namespace Triode.Launcher.Win
{
    public class Program
    {
        public static void Main(string[] args)
        {
            WinPlatform platform = new();
            platform.Run();
        }
    }
}