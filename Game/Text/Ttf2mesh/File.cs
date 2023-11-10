using System.Runtime.InteropServices;
using System.Text;
using static Game.Text.Ttf2mesh.Binding;

namespace Game.Text.Ttf2mesh
{
    public class File
    {
        internal readonly IntPtr Handle;

        public int NChars { get; protected set; }
        public int NGlyphs { get; protected set; }
        public char[] Chars { get; protected set; }
        public uint[] Char2glyph { get; protected set; }
        public Glyph[] Glyphs { get; protected set; }
        public string FileName { get; protected set; }
        public uint GlyfCsum { get; protected set; }
        public uint[] Ubranges { get; protected set; }

        public HeadT Head { get; protected set; }
        public OS2T OS2 { get; protected set; }
        public NamesT Names { get; protected set; }
        public HHEAT HHEA { get; protected set; }

        public IntPtr[] Userdata { get; protected set; }

        public struct HeadT
        {
            public float Rev;
            public MacStyleT MacStyle;

            internal HeadT(ttf_file.head_t raw)
            {
                Rev = raw.rev;
                MacStyle = new MacStyleT(raw.macStyle);
            }

            public struct MacStyleT
            {
                public bool Bold;
                public bool Italic;
                public bool Underline;
                public bool Outline;
                public bool Shadow;
                public bool Condensed;
                public bool Extended;

                internal MacStyleT(ttf_file.head_t.macStyle_t raw)
                {
                    Bold = raw.bold == 1;
                    Italic = raw.italic == 1;
                    Underline = raw.underline == 1;
                    Outline = raw.outline == 1;
                    Shadow = raw.shadow == 1;
                    Condensed = raw.condensed == 1;
                    Extended = raw.extended == 1;
                }
            }
        }
        public struct OS2T
        {
            public float XAvgCharWidth;
            public ushort UsWeightClass;
            public ushort UsWidthClass;
            public float YStrikeoutSize;
            public float YStrikeoutPos;
            public short SFamilyClass;
            public byte[] Panose;

            public FSSelectionT FSSelection;

            public float STypoAscender;
            public float STypoDescender;
            public float STypoLineGap;
            public float UsWinAscent;
            public float UsWinDescent;

            public struct FSSelectionT
            {
                public bool Italic;
                public bool Underscore;
                public bool Negative;
                public bool Outlined;
                public bool Strikeout;
                public bool Bold;
                public bool Regular;
                public bool Utm;
                public bool Oblique;

                internal FSSelectionT(ttf_file.os2_t.fsSelection_t raw)
                {
                    Italic = raw.italic == 1;
                    Underscore = raw.underscore == 1;
                    Negative = raw.negative == 1;
                    Outlined = raw.outlined == 1;
                    Strikeout = raw.strikeout == 1;
                    Bold = raw.bold == 1;
                    Regular = raw.regular == 1;
                    Utm = raw.utm == 1;
                    Oblique = raw.oblique == 1;
                }
            }

            internal OS2T(ttf_file.os2_t raw)
            {
                XAvgCharWidth = raw.xAvgCharWidth;
                UsWeightClass = raw.usWeightClass;
                UsWidthClass = raw.usWidthClass;
                YStrikeoutSize = raw.yStrikeoutSize;
                YStrikeoutPos = raw.yStrikeoutPos;
                SFamilyClass = raw.sFamilyClass;
                Panose = raw.panose;

                FSSelection = new FSSelectionT(raw.fsSelection);

                STypoAscender = raw.sTypoAscender;
                STypoDescender = raw.sTypoDescender;
                STypoLineGap = raw.sTypoLineGap;
                UsWinAscent = raw.usWinAscent;
                UsWinDescent = raw.usWinDescent;
            }
        }
        public struct NamesT
        {
            public string Copyright;
            public string Family;
            public string Subfamily;
            public string UniqueID;
            public string FullName;
            public string Version;
            public string PsName;
            public string Trademark;
            public string Manufacturer;
            public string Designer;
            public string Description;
            public string UrlVendor;
            public string UrlDesigner;
            public string LicenseDesc;
            public string LocenseUrl;
            public string SampleText;

            internal NamesT(ttf_file.names_t raw)
            {
                Copyright = Util.UtilFunc.GetByteString(raw.copyright);
                Family = Util.UtilFunc.GetByteString(raw.family);
                Subfamily = Util.UtilFunc.GetByteString(raw.subfamily);
                UniqueID = Util.UtilFunc.GetByteString(raw.unique_id);
                FullName = Util.UtilFunc.GetByteString(raw.full_name);
                Version = Util.UtilFunc.GetByteString(raw.version);
                PsName = Util.UtilFunc.GetByteString(raw.ps_name);
                Trademark = Util.UtilFunc.GetByteString(raw.trademark);
                Manufacturer = Util.UtilFunc.GetByteString(raw.manufacturer);
                Designer = Util.UtilFunc.GetByteString(raw.designer);
                Description = Util.UtilFunc.GetByteString(raw.description);
                UrlVendor = Util.UtilFunc.GetByteString(raw.url_vendor);
                UrlDesigner = Util.UtilFunc.GetByteString(raw.url_designer);
                LicenseDesc = Util.UtilFunc.GetByteString(raw.license_desc);
                LocenseUrl = Util.UtilFunc.GetByteString(raw.locense_url);
                SampleText = Util.UtilFunc.GetByteString(raw.sample_text);
            }
        }
        public struct HHEAT
        {
            public float Ascender;
            public float Descender;
            public float LineGap;
            public float AdvanceWidthMax;
            public float MinLSideBearing;
            public float MinRSideBearing;
            public float XMaxExtent;
            public float CaretSlope;

            internal HHEAT(ttf_file.hhea_t raw)
            {
                Ascender = raw.ascender;
                Descender = raw.descender;
                LineGap = raw.lineGap;
                AdvanceWidthMax = raw.advanceWidthMax;
                MinLSideBearing = raw.minLSideBearing;
                MinRSideBearing = raw.minRSideBearing;
                XMaxExtent = raw.xMaxExtent;
                CaretSlope = raw.caretSlope;
            }
        }

        internal unsafe File(IntPtr handle, bool full_info)
        {
            Handle = handle;
            ttf_file raw = Marshal.PtrToStructure<ttf_file>(handle);

            if (full_info)
            {
                NChars = raw.nchars;
                NGlyphs = raw.nglyphs;

                {
                    uint[] c = new uint[NChars];
                    Util.UtilFunc.CopyToManaged(raw.chars, c, 0, NChars);
                    Chars = c.Select(x => Convert.ToChar(x)).ToArray();
                }

                Char2glyph = new uint[NChars];
                Util.UtilFunc.CopyToManaged(raw.char2glyph, Char2glyph, 0, NChars);

                Glyphs = new Glyph[NGlyphs];
                {
                    IntPtr adr = raw.glyphs;
                    for (int i = 0; i < NGlyphs; i++)
                    {
                        Glyphs[i] = new Glyph(adr);
                        adr = IntPtr.Add(adr, Marshal.SizeOf<ttf_glyph>());
                    }
                }

                FileName = Util.UtilFunc.GetByteString(raw.filename);
                GlyfCsum = raw.glyf_csum;
                Ubranges = raw.ubranges;

                Head = new HeadT(raw.head);
                OS2 = new OS2T(raw.os2);
                Names = new NamesT(raw.names);
                HHEA = new HHEAT(raw.hhea);
                Userdata = raw.userdata;
            }
            else
            {
                NChars = 0;
                NGlyphs = 0;
                Chars = Array.Empty<char>();
                Char2glyph = Array.Empty<uint>();
                Glyphs = Array.Empty<Glyph>();

                FileName = Util.UtilFunc.GetByteString(raw.filename);
                GlyfCsum = raw.glyf_csum;
                Ubranges = raw.ubranges;

                Head = new HeadT(raw.head);
                OS2 = new OS2T(raw.os2);
                Names = new NamesT(raw.names);
                HHEA = new HHEAT(raw.hhea);
                Userdata = raw.userdata;
            }
        }
    }
}
