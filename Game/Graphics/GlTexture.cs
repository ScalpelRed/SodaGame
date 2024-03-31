using System.Numerics;
using Silk.NET.OpenGL;
using Triode.Game.General;
using Triode.Game.OtherAssets;

namespace Triode.Game.Graphics
{
    public class GlTexture
    {
        public uint Handle = 0;
        public int SizeX = 0;
        public int SizeY = 0;
        public Vector2 Size
        {
            get
            {
                return new Vector2(SizeX, SizeY);
            }
        }

        private readonly OpenGL Gl;

        public GlTexture(OpenGL gl)
        {
            Gl = gl;
            SizeX = SizeY = 0;
            Handle = 0;
        }

        public unsafe GlTexture(Vector4 color, OpenGL opengl)
        {
            Gl = opengl;
            SizeX = SizeY = 1;
            color *= 255;
            CreateGlTex(SizeX, SizeY, [(byte)color.X, (byte)color.Y, (byte)color.Z, (byte)color.W]);
        }

        public unsafe GlTexture(RawTexture raw, OpenGL opengl)
        {
            Gl = opengl;
            SizeX = raw.SizeX;
            SizeY = raw.SizeY;
            CreateGlTex(SizeX, SizeY, raw.Data);
        }

        public unsafe GlTexture(string rawtexname, OpenGL opengl) : this(opengl.Core.Assets.RawTextures.Get(rawtexname), opengl)
        {

        }

        private unsafe void CreateGlTex(int sizeX, int sizeY, byte[] data)
        {
            Handle = Gl.Api.GenTexture();
            Gl.Api.GetInteger(GetPName.TextureBinding2D, out int prevTex);
            Gl.Api.BindTexture(TextureTarget.Texture2D, Handle);
            fixed (byte* d = &data[0])
            {
                Gl.Api.TexImage2D(TextureTarget.Texture2D, 0,
                    InternalFormat.Rgba, (uint)SizeX, (uint)SizeY,
                    0, PixelFormat.Rgba, PixelType.UnsignedByte, d);
            }

            Gl.Api.GenerateMipmap(TextureTarget.Texture2D);

            Gl.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            Gl.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);

            Gl.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            Gl.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            Gl.Api.BindTexture(TextureTarget.Texture2D, (uint)prevTex);
        }
    }
}
