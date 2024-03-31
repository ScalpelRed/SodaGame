using System.Runtime.InteropServices;
using static Triode.Game.Text.Ttf2mesh.Binding;

namespace Triode.Game.Text.Ttf2mesh
{
    public class TTF2Mesh
    {
        public const string LibVersion = "1.6";

        public const int MaxFileSizeMB = 32;

        public const int WeightThin = 100;
        public const int WeightExtraLight = 200;
        public const int WeightLight = 300;
        public const int WeightNormal = 400;
        public const int WeightMedium = 500;
        public const int WeightDemibold = 600;
        public const int WeightBold = 700;
        public const int WeightExtraBold = 800;
        public const int WeightBlack = 900;

        public const byte QualityLow = 10;
        public const byte QualityNormal = 20;
        public const byte QualityHigh = 50;

        public const int FeaturesDefault = 0;
        public const int FeaturesIngoreUncriticalErrors = 1;

        public static unsafe FontList ListSystemFonts(string mask)
        {
            return new FontList(ttf_list_system_fonts(mask));
        }

        public static unsafe TTFResult LoadFromFile(string name, out File output)
        {
            IntPtr res = Marshal.AllocHGlobal(Marshal.SizeOf<ttf_file>());
            int res2 = ttf_load_from_file(name, res, false);
            res = Marshal.ReadIntPtr(res);
            output = new File(res, true);
            return (TTFResult)res2;
        }

        public static unsafe File LoadFromFile(string name, out TTFResult result)
        {
            result = LoadFromFile(name, out File res);
            return res;
        }

        public static unsafe TTFResult LoadFromMem(byte[] data, out File output)
        {
            IntPtr res = Marshal.AllocHGlobal(Marshal.SizeOf<ttf_file>());
            fixed (byte* dat = &data[0])
            {
                int res2 = ttf_load_from_mem((IntPtr)dat, data.Length, res, false);
                res = Marshal.ReadIntPtr(res);
                output = new File(res, true);
                return (TTFResult)res2;
            }
        }

        public static unsafe File LoadFromMem(byte[] data, out TTFResult result)
        {
            result = LoadFromMem(data, out File res);
            return res;
        }

        public enum TTFResult
        {
            Done = 0,
            NotEnoughMemory = 1,
            TooBigFile = 2,
            ErrorOpeningFile = 3,
            UnsupportedFileVersion = 4,
            InvalidFileStructure = 5,
            NoRequiredTablesInFile = 6,
            InvalidFileOrTableChecksum = 7,
            UnsupportedTableFormat = 8,
            UnableToCreateMesh = 9,
            GlyphHasNoOutline = 10,
            ErrorWritingFile = 11,
        }
    }
}
