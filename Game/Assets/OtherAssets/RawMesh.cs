using System.Numerics;

namespace Game.OtherAssets
{
    public partial class RawMesh
    {
        private readonly List<Vector3> Positions = [];
        private readonly List<Vector2> Texcoords = [];
        private readonly List<Vector3> Normals = [];
        private readonly List<int[]> Faces = [];

        public int VertexCount { get => Positions.Count; }
        public int FaceCount { get => Faces.Count; }

        public RawMesh()
        {

        }

        public RawMesh(string[] source)
        {
            List<Vector3> pos = [];
            List<Vector2> texc = [];
            List<Vector3> norm = [];
            HashSet<Vertex> verts = [];
            SortedDictionary<Vertex, int> vertInds = [];
            List<Vertex[]> faces = [];

            foreach (string v in source)
            {
                string[] words = v.Replace('.', ',').Split(' ', '/');

                switch (words[0])
                {
                    case "v":
                        pos.Add(new Vector3(float.Parse(words[1]), float.Parse(words[2]), float.Parse(words[3])));
                        break;
                    case "vt":
                        texc.Add(new Vector2(float.Parse(words[1]), float.Parse(words[2])));
                        break;
                    case "vn":
                        norm.Add(new Vector3(float.Parse(words[1]), float.Parse(words[2]), float.Parse(words[3])));
                        break;
                    case "f":
                        List<Vertex> face = [];
                        for (int i = 1; i < words.Length; i += 3)
                        {
                            Vertex vert = new(int.Parse(words[i]), int.Parse(words[i + 1]), int.Parse(words[i + 2]));
                            if (verts.Add(vert))
                            {
                                vertInds.Add(vert, VertexCount);
                                Positions.Add(pos[vert.Position - 1]);
                                Texcoords.Add(texc[vert.Texcoord - 1]);
                                Normals.Add(norm[vert.Normal - 1]);
                            }
                            face.Add(vert);
                        }
                        faces.Add(face.ToArray());
                        break;
                }
            }

            foreach (Vertex[] face in faces)
            {
                int[] inds = new int[face.Length];
                for (int i = 0; i < inds.Length; i++) inds[i] = vertInds[face[i]];
                Faces.Add(inds);
            }
        }

        public Vector3[] GetPosArray() => Positions.ToArray();
        public Vector2[] GetTexcoordArray() => Texcoords.ToArray();
        public Vector3[] GetNormalArray() => Normals.ToArray();
        public int[][] GetIndexArray() => Faces.ToArray();


        public Vector3 GetVertexPos(int index)
        {
            if (index < 0) throw new IndexOutOfRangeException($"Vertex index must be greater that zero (got {index})");
            else if (index >= VertexCount) throw new IndexOutOfRangeException($"This mesh has only {VertexCount} verticies (got index {index})");
            return Positions[index];
        }

        public Vector2 GetVertexTexcoord(int index)
        {
            if (index < 0) throw new IndexOutOfRangeException($"Vertex index must be greater that zero (got {index})");
            else if (index >= VertexCount) throw new IndexOutOfRangeException($"This mesh has only {VertexCount} verticies (got index {index})");
            return Texcoords[index];
        }

        public Vector3 GetVertexNormal(int index)
        {
            if (index < 0) throw new IndexOutOfRangeException($"Vertex index must be greater that zero (got {index})");
            else if (index >= VertexCount) throw new IndexOutOfRangeException($"This mesh has only {VertexCount} verticies (got index {index})");
            return Normals[index];
        }

        public int[] GetFace(int index)
        {
            if (index < 0) throw new IndexOutOfRangeException($"Face index must be greater that zero (got {index})");
            else if (index >= FaceCount) throw new IndexOutOfRangeException($"This mesh has only {FaceCount} faces (got index {index})");
            return Faces[index];
        }


        public void SetVertexPos(int index, Vector3 pos)
        {
            if (index < 0) throw new IndexOutOfRangeException($"Vertex index must be greater that zero (got {index})");
            else if (index >= VertexCount) throw new IndexOutOfRangeException($"This mesh has only {VertexCount} verticies (got index {index})");
            Positions[index] = pos;
        }

        public void SetVertexTexcoord(int index, Vector2 texcoord)
        {
            if (index < 0) throw new IndexOutOfRangeException($"Vertex index must be greater that zero (got {index})");
            else if (index >= VertexCount) throw new IndexOutOfRangeException($"This mesh has only {VertexCount} verticies (got index {index})");
            Texcoords[index] = texcoord;
        }

        public void SetVertexNormal(int index, Vector3 normal)
        {
            if (index < 0) throw new IndexOutOfRangeException($"Vertex index must be greater that zero (got {index})");
            else if (index >= VertexCount) throw new IndexOutOfRangeException($"This mesh has only {VertexCount} verticies (got index {index})");
            Normals[index] = normal;
        }

        public void SetFaceInds(int index, params int[] inds)
        {
            if (index < 0) throw new IndexOutOfRangeException($"Face index must be greater that zero (got {index})");
            else if (index >= FaceCount) throw new IndexOutOfRangeException($"This mesh has only {FaceCount} faces (got index {index})");

            Faces[index] = inds;
        }


        public int AddVertex(Vector3 pos, Vector2 texcoord, Vector3 normal)
        {
            Positions.Add(pos);
            Texcoords.Add(texcoord);
            Normals.Add(normal);
            return VertexCount - 1;
        }

        public int AddFace(params int[] inds)
        {
            Faces.Add(inds);
            return FaceCount - 1;
        }


        protected struct Vertex : IComparable
        {
            public int Position;
            public int Texcoord;
            public int Normal;

            public Vertex(int pos, int texc, int norm)
            {
                Position = pos;
                Texcoord = texc;
                Normal = norm;
            }

            public readonly int CompareTo(object? obj)
            {
                Vertex v = (Vertex)obj!;
                int c = Position.CompareTo(v.Position);
                if (c != 0) return c;
                c = Texcoord.CompareTo(v.Texcoord);
                if (c != 0) return c;
                c = Normal.CompareTo(v.Normal);
                return c;
            }
        }
    }
}
