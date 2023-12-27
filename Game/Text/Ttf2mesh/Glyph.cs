using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using static Game.Text.Ttf2mesh.Binding;
using static Game.Text.Ttf2mesh.TTF2Mesh;

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

        public Vector2 BoundP { get; protected set; }
        public Vector2 BoundN { get; protected set; }

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

            {
                Vector2 boundp;
                Vector2 boundn;

                // it's faster than using max and min
                if (raw.xbounds[0] > raw.xbounds[1])
                {
                    boundp.X = raw.xbounds[0];
                    boundn.X = raw.xbounds[1];
                }
                else
                {
                    boundp.X = raw.xbounds[1];
                    boundn.X = raw.xbounds[0];
                }

                if (raw.ybounds[0] > raw.ybounds[1])
                {
                    boundp.Y = raw.ybounds[0];
                    boundn.Y = raw.ybounds[1];
                }
                else
                {
                    boundp.Y = raw.ybounds[1];
                    boundn.Y = raw.ybounds[0];
                }

                BoundP = boundp;
                BoundN = boundn;
            }

            Advance = raw.advance;
            LBearing = raw.lbearing;
            RBearing = raw.rbearing;

            if (raw.outline != IntPtr.Zero)
            {
                Outline = new Outline(raw.outline);
                HasOutline = true;
            }
            else
            {
                Outline = null;
                HasOutline = false;
            }

            Userdata = raw.userdata;
        }

        public unsafe TTFResult ToMesh(byte quality, int features, out Mesh? output)
        {
            if (HasOutline)
            {
                IntPtr res = Marshal.AllocHGlobal(Marshal.SizeOf<ttf_mesh>());
                int res2 = ttf_glyph2mesh(Handle, res, quality, features);
                if (res2 == (int)TTFResult.GlyphHasNoOutline)
                {
                    output = null;
                }
                else
                {
                    res = Marshal.ReadIntPtr(res);
                    output = new Mesh(res, Outline!);
                }
                return (TTFResult)res2;
            }

            output = null;
            return TTFResult.GlyphHasNoOutline;
        }

        public unsafe Mesh? ToMesh(byte quality, int features, out TTFResult result)
        {
            result = ToMesh(quality, features, out Mesh? res);
            return res;
        }
    }
}
