using Triode.Game.OtherAssets;
using System.Numerics;
using System.Runtime.InteropServices;
using static Triode.Game.Text.Ttf2mesh.Binding;

namespace Triode.Game.Text.Ttf2mesh
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

            public override string ToString()
            {
                return $"[{V1} {V2} {V3}]";
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
                    ttf_mesh.vert_t vert = Marshal.PtrToStructure<ttf_mesh.vert_t>(adr);
                    Vert[i] = new Vector2(vert.x, vert.y);
                    adr = IntPtr.Add(adr, Marshal.SizeOf<ttf_mesh.vert_t>());
                }
            }

            Faces = new Face[NFaces];
            {
                IntPtr adr = raw.faces;
                for (int i = 0; i < NFaces; i++)
                {
                    ttf_mesh.faces_t face = Marshal.PtrToStructure<ttf_mesh.faces_t>(adr);
                    Faces[i] = new Face(face);
                    adr = IntPtr.Add(adr, Marshal.SizeOf<ttf_mesh.faces_t>());
                }
            }

            Outline = outline;
        }

        ~Mesh()
        {
            ttf_free_mesh(Handle);
        }

        public RawMesh ToRawMesh()
        {
            RawMesh res = new();

            foreach (Vector2 vert in Vert)
            {
                res.AddVertex(new Vector3(vert, 0), vert, Vector3.UnitZ);
            }

            foreach (Face face in Faces)
            {
                res.AddFace(face.V1, face.V2, face.V3);
            }

            return res;
        }
    }
}
