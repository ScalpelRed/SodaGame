using Android.Content.PM;
using Silk.NET.Windowing.Sdl.Android;

namespace Triode.PlAndroid;

[Activity
(
    Label = "@string/app_name",
    MainLauncher = true,
    ConfigurationChanges = ConfigChangesFlags,
    ScreenOrientation = ScreenOrientation.Sensor,
    Theme = "@android:style/Theme.Black.NoTitleBar.Fullscreen"
)]
public class MainActivity : SilkActivity
{
    protected override void OnRun() => new Core(new AndroidPlatform(this)).Run();
}
