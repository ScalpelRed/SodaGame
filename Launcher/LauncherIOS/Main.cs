using Silk.NET.Windowing.Sdl.iOS;
using Triode;
using Triode.iOS;

// Initialize and run Core
SilkMobile.RunApp(args, (_) => new Core(new iOSPlatform()).Run());