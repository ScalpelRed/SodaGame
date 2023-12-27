using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Runtime.InteropServices;
using SixLabors.ImageSharp.Advanced;
using Game.Assets;

namespace Game.OtherAssets
{
    public class RawTexture
    {
        public readonly int SizeX;
        public readonly int SizeY;

        public byte[] Data;

        public RawTexture(string name, GameCore core)
        {
            Image<Rgba32> image;
            try
            {
                image = Image.Load<Rgba32>(core.Platform.IO.GetReadableStream("textures/" + name + ".png"));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw new AssetNotFoundException("texture", name);
            }

            image.Mutate(x => x.Flip(FlipMode.Vertical));
            SizeX = image.Width;
            SizeY = image.Height;
            Data = new byte[SizeX * SizeY * 4];
            image.CopyPixelDataTo(Data);
            image.Dispose();
        }

        public RawTexture(int sizeX, int sizeY)
        {
            SizeX = sizeX;
            SizeY = sizeY;
            Data = new byte[sizeX * sizeY];
        }

        public Vector4 GetPixel(int x, int y)
        {
            int pos0 = (y * SizeX + x) * 4;
            return new(Data[pos0], Data[pos0 + 1], Data[pos0 + 2], Data[pos0 + 3]);
        }
    }
}
