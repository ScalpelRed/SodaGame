using Game.OtherAssets;
using Game.Util;
using Silk.NET.OpenGL;

namespace Game.Graphics
{
    public class GlMesh
    {
        public uint VBOHandle;
        public uint VAOHandle;
        public uint EBOHandle;

        public uint VertexCount;
        public int VertsPerPolygon;

        public unsafe GlMesh(OpenGL gl, int vertsPerPolygon, int vertexCount, int[] indexArray, params float[][] data)
        {
            Build(gl, vertsPerPolygon, vertexCount, indexArray, data);
        }

        public unsafe GlMesh(OpenGL gl, RawMesh source)
        {
            Vertex[] verts = source.GetVertexArray();

            Build(gl, source.VertsPerPolygon, source.VertexCount, source.GetIndexArray(), 
                RawMesh.GetPositionArray(verts), RawMesh.GetTexcoordArray(verts), RawMesh.GetNormalArray(verts));
        }

        protected unsafe void Build(OpenGL gl, int vertsPerPolygon, int vertexCount, int[] indexArray, params float[][] data)
        {
            Attribute[] attributes = new Attribute[data.Length];
            float[] vertexArray;

            {
                List<float> vertexArrayList = new();
                uint offset = 0;
                for (uint i = 0; i < data.Length; i++)
                {
                    Attribute attribute = new(data[i], vertsPerPolygon, vertexCount, i, offset);
                    offset += (uint)(attribute.Data.Length * sizeof(float));
                    attributes[i] = attribute;

                    vertexArrayList.AddRange(attribute.Data);
                }
                vertexArray = vertexArrayList.ToArray();
            }

            VAOHandle = gl.Api.GenVertexArray();
            VBOHandle = gl.Api.GenBuffer();
            EBOHandle = gl.Api.GenBuffer();

            gl.Api.BindVertexArray(VAOHandle);

            fixed (void* d = &vertexArray[0])
            {
                gl.Api.BindBuffer(BufferTargetARB.ArrayBuffer,
                    VBOHandle);
                gl.Api.BufferData(BufferTargetARB.ArrayBuffer,
                (nuint)(vertexArray.Length * sizeof(float)), d,
                BufferUsageARB.StaticDraw);
            }

            foreach (Attribute a in attributes)
            {
                gl.Api.VertexAttribPointer(a.Index, a.Size, VertexAttribPointerType.Float, false, a.Stride, (void*)a.Offset);
                gl.Api.EnableVertexAttribArray(a.Index);
            }

            fixed (void* d = &indexArray[0])
            {
                gl.Api.BindBuffer(BufferTargetARB.ElementArrayBuffer,
                    EBOHandle);
                gl.Api.BufferData(BufferTargetARB.ElementArrayBuffer,
                (nuint)(indexArray.Length * sizeof(int)), d,
                BufferUsageARB.StaticDraw);
            }

            gl.Api.BindBuffer(BufferTargetARB.ArrayBuffer, 0);
            gl.Api.BindVertexArray(0);

            VertexCount = (uint)indexArray.Length;
            VertsPerPolygon = vertsPerPolygon;
        }

        protected struct Attribute
        {
            public uint Index;
            public int Size;
            public uint Stride;
            public uint Offset;
            public float[] Data;

            public Attribute(float[] data, int vertsPerPolygon, int vertexCount, uint index, uint offset)
            {
                if (data.Length % vertsPerPolygon != 0) throw new Exception(
                    $"Attribute {index}: length of the attribute array ({data.Length}) must be a multiple of the number of vertices per polygon ({vertsPerPolygon})");

                Index = index;
                Size = data.Length / vertexCount;
                Stride = (uint)(Size * sizeof(float));
                Offset = offset;
                Data = data;
            }
        }
    }
}
