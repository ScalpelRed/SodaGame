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

        private unsafe Outline(IntPtr handle, ttf_outline raw)
        {
            Handle = handle;
            
            TotalPoints = raw.total_points;
            NContours = raw.ncontours;

            Cont = new Contour[NContours];
            {
                IntPtr adr = raw.cont0;
                for (int i = 0; i < NContours; i++)
                {
                    Cont[i] = new Contour(adr);
                    adr = IntPtr.Add(adr, Marshal.SizeOf<ttf_outline.ttf_contour>());
                }
            }
        }

        public static Outline? TryMarshal(IntPtr handle)
        {
            ttf_outline raw;
            try
            {
                raw = Marshal.PtrToStructure<ttf_outline>(handle);
            }
            catch (NullReferenceException)
            {
                return null;
            }

            return new Outline(handle, raw);
        }
    }
}
