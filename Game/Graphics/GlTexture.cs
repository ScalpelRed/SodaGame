using System.Numerics;
using Silk.NET.OpenGL;
using Game.Main;
using Game.OtherAssets;

namespace Game.Graphics
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

        private readonly OpenGL? Gl;

        public GlTexture()
        {
            SizeX = SizeY = 0;
            Handle = 0;
        }

        public unsafe GlTexture(Vector4 color, OpenGL opengl)
        {
            SizeX = SizeY = 1;

            color *= 255;
            byte[] data = [(byte)color.X, (byte)color.Y, (byte)color.Z, (byte)color.W];

            Handle = opengl.Api.GenTexture();
            opengl.Api.BindTexture(TextureTarget.Texture2D, Handle);
            fixed (byte* d = &data[0])
            {
                opengl.Api.TexImage2D(TextureTarget.Texture2D, 0,
                    InternalFormat.Rgba, (uint)SizeX, (uint)SizeY,
                    0, PixelFormat.Rgba, PixelType.UnsignedByte, d);
            }

            opengl.Api.GenerateMipmap(TextureTarget.Texture2D);

            opengl.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            opengl.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);

            opengl.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            opengl.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        }

        public unsafe GlTexture(RawTexture raw, OpenGL opengl)
        {
            Gl = opengl;

            SizeX = raw.SizeX;
            SizeY = raw.SizeY;
            byte[] data = raw.Data;

            Handle = opengl.Api.GenTexture();
            opengl.Api.BindTexture(TextureTarget.Texture2D, Handle);
            fixed (byte* d = &data[0])
            {
                opengl.Api.TexImage2D(TextureTarget.Texture2D, 0,
                    InternalFormat.Rgba, (uint)SizeX, (uint)SizeY,
                    0, PixelFormat.Rgba, PixelType.UnsignedByte, d);
            }

            opengl.Api.GenerateMipmap(TextureTarget.Texture2D);

            opengl.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Nearest);
            opengl.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMinFilter.Linear);

            opengl.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            opengl.Api.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);
        }

        public unsafe GlTexture(string rawtexname, OpenGL opengl) : this(opengl.Core.Assets.RawTextures.Get(rawtexname), opengl)
        {

        }
    }
}
