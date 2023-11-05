using System.Numerics;
using System.Linq;
using Game.Phys;
using Game.Util;

namespace Game.OtherAssets
{
    public partial class RawMesh
    {
        private readonly List<Vertex> Verts = new();
        private readonly List<int>[] Polygons = Array.Empty<List<int>>();
        public int VertsPerPolygon;
        public int PolygonCount { get; private set; }
        public int VertexCount
        {
            get => Verts.Count;
        }

        public RawMesh(int vertsPerPolygon)
        {
            VertsPerPolygon = vertsPerPolygon;
            Polygons = new List<int>[vertsPerPolygon];
            for (int i = 0; i < Polygons.Length; i++)
                Polygons[i] = new List<int>();
        }

        public RawMesh(string[] source)
        {
            List<Vector3> coords = new();
            List<Vector2> texcoords = new();
            List<Vector3> normals = new();

            bool VPPnotSet = true;

            foreach (string v in source)
            {
                string[] words = v.Replace('.', ',').Split(' ', '/');

                switch (words[0])
                {
                    case "v":
                        coords.Add(new Vector3(
                            float.Parse(words[1]),
                            float.Parse(words[2]),
                            float.Parse(words[3])));
                        break;
                    case "vt":
                        texcoords.Add(new Vector2(
                            float.Parse(words[1]),
                            float.Parse(words[2])));
                        break;
                    case "vn":
                        normals.Add(new Vector3(
                            float.Parse(words[1]),
                            float.Parse(words[2]),
                            float.Parse(words[3])));
                        break;
                    case "f":
                        if (VPPnotSet)
                        {
                            VPPnotSet = false;
                            VertsPerPolygon = (words.Length - 1) / 3;
                            Polygons = new List<int>[VertsPerPolygon];
                            for (int i = 0; i < Polygons.Length; i++)
                                Polygons[i] = new List<int>();
                        }

                        for (int vertexWord = 1; vertexWord < words.Length; vertexWord += 3)
                        {
                            Verts.Add(new Vertex(
                                coords[int.Parse(words[vertexWord]) - 1],
                                texcoords[int.Parse(words[vertexWord + 1]) - 1],
                                normals[int.Parse(words[vertexWord + 2]) - 1]
                                ));
                            Polygons[vertexWord / 3].Add(Verts.Count - 1);
                        }

                        PolygonCount++;
                        break;
                }
            }
        }

        public void AddVertex(Vector3 coords, Vector2 texcoords, Vector3 normal)
        {
            Verts.Add(new Vertex(coords, texcoords, normal));
        }

        public int AddPolygon(params int[] verts)
        {
            if (VertsPerPolygon != verts.Length)
            {
                throw new Exception("This mesh supports only " + VertsPerPolygon
                    + " vertices per polygon, but tried to add polygon of "
                    + verts.Length / 3);
            }

            for (int i = 0; i < VertsPerPolygon; i++)
            {
                Polygons[i].Add(verts[i]);
            }
            return ++PolygonCount;
        }

        public int ApplyPolygon()
        {
            if (Verts.Count < VertsPerPolygon)
                throw new Exception("Not enouth verticies to apply polygon: "
                    + VertsPerPolygon + " expected, but has " + Verts.Count);

            for (int i = 0; i < VertsPerPolygon; i++)
            {
                Polygons[i].Add(Verts.Count - VertsPerPolygon + i);
            }
            return ++PolygonCount;
        }

        public Vertex[] GetVertexArray()
        {
            return Verts.ToArray();
        }

        public int[] GetIndexArray()
        {
            int[] res = new int[PolygonCount * VertsPerPolygon];

            for (int pol = 0; pol < PolygonCount; pol++)
            {
                int polPos = pol * VertsPerPolygon;
                for (int vert = 0; vert < VertsPerPolygon; vert++)
                {
                    res[polPos + vert] = Polygons[vert][pol];
                }
            }

            return res;
        }

        /*public PhysicalMesh2D ToPhysicalMesh()
        {
            PhysicalMesh2D res = new();

            for (int pol = 0; pol < PolygonCount; pol++)
            {
                Vertices polygon = new Vertices();
                for (int vert = 0; vert < VertsPerPolygon; vert++)
                {
                    polygon.Add(Verts[Polygons[vert][pol]].Position.XY().ToXna());
                }
                res.AddPolygon(polygon);
            }

            return res;
        }*/

        public override string ToString()
        {
            return $"{Verts.Count}/{Polygons[0].Count}/{VertsPerPolygon}";
        }

        public static float[] GetPositionArray(Vertex[] verts)
        {
            return verts.SelectMany(v => new float[] { v.Position.X, v.Position.Y, v.Position.Z }).ToArray();
        }

        public static float[] GetTexcoordArray(Vertex[] verts)
        {
            return verts.SelectMany(v => new float[] { v.Texcoords.X, v.Texcoords.Y }).ToArray();
        }

        public static float[] GetNormalArray(Vertex[] verts)
        {
            return verts.SelectMany(v => new float[] { v.Normal.X, v.Normal.Y, v.Normal.Z }).ToArray();
        }
    }
}
