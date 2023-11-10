﻿using Game.OtherAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Game.Text.Ttf2mesh.Binding;

namespace Game.Text.Ttf2mesh
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

        public static unsafe TTFResult GlyphToMesh(Glyph glyph, byte quality, int features, out Mesh? output)
        {
            if (glyph.HasOutline) 
            {
                IntPtr res = Marshal.AllocHGlobal(Marshal.SizeOf<ttf_mesh>());
                int res2 = ttf_glyph2mesh(glyph.Handle, res, quality, features);
                if (res2 == (int)TTFResult.GlyphHasNoOutline)
                {
                    output = null;
                }
                else
                {
                    res = Marshal.ReadIntPtr(res);
                    output = new Mesh(res, glyph.Outline!);
                }
                return (TTFResult)res2;
            }

            output = null;
            return TTFResult.GlyphHasNoOutline;
        }

        public static unsafe Mesh? GlyphToMesh(Glyph glyph, byte quality, int features, out TTFResult result)
        {
            result = GlyphToMesh(glyph, quality, features, out Mesh? res);
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