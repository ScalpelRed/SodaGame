using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Game.Text.Ttf2mesh.Binding;

namespace Game.Text.Ttf2mesh
{
    public class Outline
    {
        internal readonly IntPtr Handle;

        public int TotalPoints { get; protected set; }
        public int NContours { get; protected set; }
        public Contour[] Cont { get; protected set; }

        public struct Contour
        {
            public int Length;
            public int SubGlyphID;
            public int SubGlyphOrder;
            public Point[] Pt;

            internal Contour(IntPtr handle)
            {
                ttf_outline.ttf_contour raw = Marshal.PtrToStructure<ttf_outline.ttf_contour>(handle);

                Length = raw.length;
                SubGlyphID = raw.subglyph_id;
                SubGlyphOrder = raw.subglyph_order;

                Pt = new Point[Length];
                {
                    IntPtr adr = raw.pt;
                    for (int i = 0; i < Length; i++)
                    {
                        Pt[i] = new Point(adr);
                        adr = IntPtr.Add(adr, Marshal.SizeOf<ttf_point>());
                    }
                }
            }
        }

        public unsafe Outline(IntPtr handle)
        {
            Handle = handle;

            ttf_outline raw = Marshal.PtrToStructure<ttf_outline>(handle);

            TotalPoints = raw.total_points;
            NContours = raw.ncontours;

            Cont = new Contour[NContours];
            {
                IntPtr adr = handle + 8; // The values are written in array that have predefined size of 1, and I don't know how to marshal it correctly
                for (int i = 0; i < NContours; i++)
                {
                    Cont[i] = new Contour(adr);
                    adr = IntPtr.Add(adr, Marshal.SizeOf<ttf_outline.ttf_contour>());
                }
            }
        }
    }
}
