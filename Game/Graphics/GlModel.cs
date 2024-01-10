using Silk.NET.OpenGL;

namespace Game.Graphics
{
    public class GlModel
    {
        public readonly OpenGL Gl;

        public GlMesh Mesh;
        public GlTexture Texture;
        public GlShader Shader;

        public PrimitiveType RenderMode;


        public GlModel(OpenGL gl, GlMesh mesh, GlTexture texture, GlShader shader)
        {
            Gl = gl;

            Mesh = mesh;
            Shader = shader;
            Texture = texture;
            ResetRenderMode();
        }

        public GlModel(OpenGL gl, GlMesh mesh, GlShader shader) : this(gl, mesh, gl.Core.Assets.Textures.Get(""), shader)
        {
            
        }

        public GlModel(OpenGL gl, GlShader shader) : this(gl, gl.Core.Assets.GlMeshes.Get("sprite"), shader)
        {

        }

        public GlModel(OpenGL gl, GlTexture texture, GlShader shader) : this(gl, gl.Core.Assets.GlMeshes.Get("sprite"), texture, shader)
        {

        }


        public unsafe void ResetRenderMode()
        {
            RenderMode = Mesh.VertsPerPolygon switch
            {
                1 => PrimitiveType.Points,
                2 => PrimitiveType.Lines,
                3 => PrimitiveType.Triangles,
                4 => PrimitiveType.Quads, // does not work
                _ => PrimitiveType.Triangles,
            };
        }

        public unsafe void Draw()
        {
            if (Shader.Handle == 0) return;

            Shader.ApplyUniforms();
            Gl.SetActiveTexture(Texture);
            Gl.SetActiveMesh(Mesh);

            Gl.Api.DrawElements(RenderMode, Mesh.VertexCount, DrawElementsType.UnsignedInt, (void*)null);

            //Gl.Core.Log(Gl.Api.GetError());
        }
    }
}
