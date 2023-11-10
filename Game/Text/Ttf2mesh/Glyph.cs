using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using static Game.Text.Ttf2mesh.Binding;

namespace Game.Text.Ttf2mesh
{
    public class Glyph
    {
        public readonly IntPtr Handle;

        public int Index { get; protected set; }
        public char Symbol { get; protected set; }
        public int NPoints { get; protected set; }
        public int NContours { get; protected set; }
        public bool Composite { get; protected set; }
        public uint Reserved { get; protected set; }

        public Vector2 XBounds { get; protected set; }
        public Vector2 YBounds { get; protected set; }

        public float Advance { get; protected set; }
        public float LBearing { get; protected set; }
        public float RBearing { get; protected set; }

        public Outline? Outline { get; protected set; }
        public bool HasOutline { get; protected set; }

        public IntPtr[] Userdata { get; protected set; }

        internal unsafe Glyph(IntPtr handle)
        {
            Handle = handle;
            ttf_glyph raw = Marshal.PtrToStructure<ttf_glyph>(handle);

            Index = raw.index;
            Symbol = Convert.ToChar(raw.symbol);
            NPoints = raw.npoints;
            NContours = raw.ncontours;
            Composite = raw.composite == 1;
            Reserved = raw.reserved;

            XBounds = new Vector2(raw.xbounds[0], raw.xbounds[1]);
            YBounds = new Vector2(raw.ybounds[0], raw.ybounds[1]);

            Advance = raw.advance;
            LBearing = raw.lbearing;
            RBearing = raw.rbearing;

            Outline = Outline.TryMarshal(raw.outline);
            HasOutline = Outline is not null;

            Userdata = raw.userdata;
        }
    }
}
