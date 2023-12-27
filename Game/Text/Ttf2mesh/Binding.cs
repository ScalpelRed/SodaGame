using System.Runtime.InteropServices;

namespace Game.Text.Ttf2mesh
{
    internal class Binding
    {

#if PL_WIN64
        public const string libpath = "ttf2mesh_x64.dll";
#elif PL_WIN86 || PL_WIN32
        public const string libpath = "ttf2mesh_x86.dll";
#elif PL_ANDROID
        public const string libpath = "ttf2mesh.so";
#else
#error Unknown platform
#endif

        const int TTF_GLYPH_USERDATA = 4;
        const int TTF_FILE_USERDATA = 4;

#pragma warning disable 0649

        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct ttf_file
        {
            public int nchars;
            public int nglyphs;
            public IntPtr chars;
            public IntPtr char2glyph;
            public IntPtr glyphs;
            public IntPtr filename;
            public uint glyf_csum;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public uint[] ubranges;

            public head_t head;
            public os2_t os2;
            public names_t names;
            public hhea_t hhea;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = TTF_FILE_USERDATA)]
            public IntPtr[] userdata;

            public struct head_t
            {
                public float rev;
                public macStyle_t macStyle;

                public struct macStyle_t
                {
                    public byte bit;
                    public byte bold { get => (byte)(bit & 1); }
                    public byte italic { get => (byte)(bit & 2); }
                    public byte underline { get => (byte)(bit & 4); }
                    public byte outline { get => (byte)(bit & 8); }
                    public byte shadow { get => (byte)(bit & 16); }
                    public byte condensed { get => (byte)(bit & 32); }
                    public byte extended { get => (byte)(bit & 64); }
                }
            }

            public struct os2_t
            {
                public float xAvgCharWidth;
                public ushort usWeightClass;
                public ushort usWidthClass;
                public float yStrikeoutSize;
                public float yStrikeoutPos;
                public short sFamilyClass;
                [MarshalAs(UnmanagedType.ByValArray, SizeConst = 10)]
                public byte[] panose;

                public fsSelection_t fsSelection;

                public float sTypoAscender;
                public float sTypoDescender;
                public float sTypoLineGap;
                public float usWinAscent;
                public float usWinDescent;


                public struct fsSelection_t
                {
                    public short bit;
                    public byte italic { get => (byte)(bit & 1); }
                    public byte underscore { get => (byte)(bit & 2); }
                    public byte negative { get => (byte)(bit & 4); }
                    public byte outlined { get => (byte)(bit & 8); }
                    public byte strikeout { get => (byte)(bit & 16); }
                    public byte bold { get => (byte)(bit & 32); }
                    public byte regular { get => (byte)(bit & 64); }
                    public byte utm { get => (byte)(bit & 128); }
                    public byte oblique { get => (byte)(bit & 256); }
                }
            }

            public struct names_t
            {
                public IntPtr copyright;
                public IntPtr family;
                public IntPtr subfamily;
                public IntPtr unique_id;
                public IntPtr full_name;
                public IntPtr version;
                public IntPtr ps_name;
                public IntPtr trademark;
                public IntPtr manufacturer;
                public IntPtr designer;
                public IntPtr description;
                public IntPtr url_vendor;
                public IntPtr url_designer;
                public IntPtr license_desc;
                public IntPtr locense_url;
                public IntPtr sample_text;
            }

            public struct hhea_t
            {
                public float ascender;
                public float descender;
                public float lineGap;
                public float advanceWidthMax;
                public float minLSideBearing;
                public float minRSideBearing;
                public float xMaxExtent;
                public float caretSlope;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct ttf_glyph
        {
            public int index;
            public int symbol;
            public int npoints;
            public int ncontours;

            public uint bit;
            public byte composite { get => (byte)(bit & 1); }
            public uint reserved { get => bit & 0b11111110; }

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public float[] xbounds;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public float[] ybounds;

            public float advance;
            public float lbearing;
            public float rbearing;

            public IntPtr outline;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = TTF_GLYPH_USERDATA)]
            public IntPtr[] userdata;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal unsafe struct ttf_outline
        {
            public int total_points;
            public int ncontours;

            [StructLayout(LayoutKind.Sequential)]
            public struct ttf_contour
            {
                public int length;
                public int subglyph_id;
                public int subglyph_order;
                public IntPtr pt;
            }

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public ttf_contour[] cont;
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ttf_point
        {
            public float x;
            public float y;
            public uint bit;
            public uint spl { get => bit & 1; }
            public uint onc { get => bit & 2; }
            public uint shd { get => bit & 4; }
            public uint res { get => bit & (uint.MaxValue - 7); }
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct ttf_mesh
        {
            public int nvert;
            public int nfaces;

            public IntPtr vert;
            public IntPtr faces;

            public IntPtr outline;

            public struct vert_t
            {
                public float x;
                public float y;
            }

            public struct faces_t
            {
                public int v1;
                public int v2;
                public int v3;
            }
        }

        internal struct ttf_mesh3d
        {
            public int nvert;
            public int nfaces;

            public IntPtr vert;
            public IntPtr faces;
            public IntPtr normals;

            public IntPtr outline;

            public struct vert_t
            {
                public float x;
                public float y;
                public float z;
            }

            public struct faces_t
            {
                public int v1;
                public int v2;
                public int v3;
            }

            public struct normals_t
            {
                public float x;
                public float y;
                public float z;
            }
        };

        internal struct unicode_bmp_range
        {
            public ushort first;
            public ushort last;
            public IntPtr name;
        };

#pragma warning restore 0649

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ttf_load_from_mem(IntPtr data, int size, IntPtr output, bool headers_only);

        [DllImport(libpath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        internal static extern int ttf_load_from_file(string filename, IntPtr output, bool headers_only);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ttf_list_fonts(IntPtr directories, int dir_count, IntPtr mask);

        [DllImport(libpath, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        internal static extern IntPtr ttf_list_system_fonts(string mask);

        /*[DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ttf_list_match(IntPtr list, IntPtr deflt, IntPtr requirements, ...);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ttf_list_match_id(IntPtr list, IntPtr requirements, ...);*/

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ttf_find_glyph(IntPtr ttf, uint utf32_char);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ttf_splitted_outline(IntPtr glyph);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ttf_linear_outline(IntPtr glyph, byte quality);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ttf_outline_evenodd_base(IntPtr outline, [MarshalAs(UnmanagedType.R4, SizeConst = 2)] float[] point, int contour, IntPtr dist);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ttf_outline_evenodd(IntPtr outline, [MarshalAs(UnmanagedType.R4, SizeConst = 2)] float[] point, int subglyph);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern bool ttf_outline_contour_info(IntPtr outline, int subglyph, int contour, int test_point, IntPtr nested_to);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern IntPtr ttf_glyph2svgpath(IntPtr glyph, float xscale, float yscale);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ttf_glyph2mesh(IntPtr glyph, IntPtr output, byte quality, int features);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ttf_glyph2mesh3d(IntPtr glyph, IntPtr output, byte quality, int features, float depth);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern int ttf_export_to_obj(IntPtr ttf, IntPtr file_name, byte quality);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ttf_free_outline(IntPtr outline);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ttf_free_mesh(IntPtr mesh);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ttf_free_mesh3d(IntPtr mesh);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ttf_free_list(IntPtr list);

        [DllImport(libpath, CallingConvention = CallingConvention.Cdecl)]
        internal static extern void ttf_free(IntPtr ttf);
    }
}
