using Game.OtherAssets;
using Game.Util;
using Silk.NET.OpenGL;
using System.Numerics;

namespace Game.Graphics
{
    public class GlMesh
    {
        public uint VBOHandle { get; protected set; }
        public uint VAOHandle { get; protected set; }
        public uint EBOHandle { get; protected set; }

        public uint VertexCount { get; protected set; }
        public int VertsPerPolygon { get; protected set; }

#nullable disable
        protected Attribute[] Attributes;
#nullable enable

        public GlMesh()
        {
            VBOHandle = 0;
            VAOHandle = 0;
            EBOHandle = 0;
            VertexCount = 0;
            VertsPerPolygon = 0;
            Attributes = Array.Empty<Attribute>();
        }

        public bool IsEmpty() => VertexCount == 0;

        public unsafe GlMesh(OpenGL gl, int vertsPerPolygon, int vertexCount, int[] indexArray, params float[][] data)
        {
            Build(gl, vertsPerPolygon, vertexCount, indexArray, data);
        }

        public unsafe GlMesh(OpenGL gl, RawMesh source)
        {
            int[][] inds = source.GetIndexArray();

            Build(gl, inds[0].Length, source.VertexCount, UtilFunc.ToLinear(inds),
                ToLinearArray(source.GetPosArray()),
                ToLinearArray(source.GetTexcoordArray()),
                ToLinearArray(source.GetNormalArray()));
        }

        public unsafe GlMesh(OpenGL gl, RawMesh source, params float[][] additionalData)
        {
            int[][] inds = source.GetIndexArray();

            Build(gl, inds[0].Length, source.VertexCount, UtilFunc.ToLinear(inds),
                new float[][] {
                    ToLinearArray(source.GetPosArray()),
                    ToLinearArray(source.GetTexcoordArray()),
                    ToLinearArray(source.GetNormalArray()) }
                .Concat(additionalData).ToArray());
        }

        protected virtual unsafe void Build(OpenGL gl, int vertsPerPolygon, int vertexCount, int[] indexArray, params float[][] data)
        {
            float[] vertexArray;
            Attributes = new Attribute[data.Length];
            {
                List<float> vertexArrayList = new();
                uint offset = 0;
                for (uint i = 0; i < data.Length; i++)
                {
                    Attribute attribute = new(data[i], vertexCount, i, offset);
                    offset += (uint)(attribute.Data.Length * sizeof(float));
                    Attributes[i] = attribute;

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

            foreach (Attribute a in Attributes)
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

        public static float[] ToLinearArray(Vector3[] arr)
        {
            List<float> res = new();
            foreach (Vector3 vert in arr)
            {
                res.Add(vert.X);
                res.Add(vert.Y);
                res.Add(vert.Z);
            }
            return res.ToArray();
        }

        public static float[] ToLinearArray(Vector2[] arr)
        {
            List<float> res = new();
            foreach (Vector2 vert in arr)
            {
                res.Add(vert.X);
                res.Add(vert.Y);
            }
            return res.ToArray();
        }

        protected struct Attribute
        {
            public uint Index;
            public int Size;
            public uint Stride;
            public uint Offset;
            public float[] Data;

            public Attribute(float[] data, int vertexCount, uint index, uint offset)
            {
                if (data.Length % vertexCount != 0) throw new Exception(
                    $"Extra values are not allowed (got {data.Length} values, expected multiple of {vertexCount} at attribute {index})");

                Index = index;
                Size = data.Length / vertexCount;
                Stride = (uint)(Size * sizeof(float));
                Offset = offset;
                Data = data;
            }
        }
    }
}
