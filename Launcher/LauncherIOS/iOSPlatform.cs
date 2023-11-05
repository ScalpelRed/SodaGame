using Silk.NET.OpenGLES;
using Silk.NET.Windowing;

namespace Triode.iOS;

// iOS PLATFORM IS NOT WORKING YET

public class iOSPlatform : Platform
{
    IView view;
    public override GL CreateGL() => GL.GetApi(view ?? throw new Exception("View was not initialized"));

    public override IView CreateView() => view = Window.GetView(new()
    {
        API = new GraphicsAPI(ContextAPI.OpenGLES, new(2, 0))
    });

    public override FileStream CreateWriteable(string name)
    {
        throw new NotImplementedException();
    }

    public override void Debug(object text)
    {
        throw new NotImplementedException();
    }

    public override FileStream GetReadableStream(string name)
    {
        throw new NotImplementedException();
    }

    public override StreamReader GetReadableStreamReader(string name)
    {
        throw new NotImplementedException();
    }

    public override (int, int, byte[]) GetTextureData(string name)
    {
        throw new NotImplementedException();
    }

    public override FileStream GetWriteableStream(string name)
    {
        throw new NotImplementedException();
    }

    public override StreamReader GetWriteableStreamReader(string name)
    {
        throw new NotImplementedException();
    }

    public override StreamWriter GetWriteableStreamWriter(string name)
    {
        throw new NotImplementedException();
    }

    public override void LinkToApp(global::Silk.NET.Windowing.IView view)
    {
        throw new NotImplementedException();
    }

    public override bool ReadableExists(string name)
    {
        throw new NotImplementedException();
    }

    public override string[] ReadAllLinesReadable(string name)
    {
        throw new NotImplementedException();
    }

    public override string[] ReadAllLinesWriteable(string name)
    {
        throw new NotImplementedException();
    }

    public override string ReadAllTextReadable(string name)
    {
        throw new NotImplementedException();
    }

    public override string ReadAllTextWriteable(string name)
    {
        throw new NotImplementedException();
    }

    public override bool WriteableExists(string name)
    {
        throw new NotImplementedException();
    }
}