using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Game.Text.Ttf2mesh.Binding;

namespace Game.Text.Ttf2mesh
{
    public class Mesh
    {
        internal readonly IntPtr Handle;

        public int NVert { get; protected set; }
        public int NFaces { get; protected set; }

        public Vector2[] Vert { get; protected set; }
        public Face[] Faces { get; protected set; }

        public Outline Outline;

        public struct Face
        {
            public int V1;
            public int V2;
            public int V3;

            internal Face(ttf_mesh.faces_t raw)
            {
                V1 = raw.v1;
                V2 = raw.v2;
                V3 = raw.v3;
            }
        }

        internal Mesh(IntPtr handle, Outline outline)
        {
            Handle = handle;
            ttf_mesh raw = Marshal.PtrToStructure<ttf_mesh>(handle);

            NVert = raw.nvert;
            NFaces = raw.nfaces;

            Vert = new Vector2[NVert];
            {
                IntPtr adr = raw.vert;
                for (int i = 0; i < NVert; i++)
                {
                    ttf_mesh.vert_t vert = Marshal.PtrToStructure<ttf_mesh.vert_t>(handle);
                    Vert[i] = new Vector2(vert.x, vert.y);
                }
            }

            Faces = new Face[NFaces];
            {
                IntPtr adr = raw.faces;
                for (int i = 0; i < NFaces; i++)
                {
                    ttf_mesh.faces_t face = Marshal.PtrToStructure<ttf_mesh.faces_t>(handle);
                    Faces[i] = new Face(face);
                }
            }

            Outline = outline;
        }

        ~Mesh()
        {
            ttf_free_mesh(Handle);
        }
    }
}
