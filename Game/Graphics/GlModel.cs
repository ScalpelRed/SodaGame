using System.Diagnostics;
using System.Numerics;
using Silk.NET.OpenGL;

namespace Game.Graphics
{
    public class GlModel
    {
        private static uint currentBuffer;
        private static uint currentTexture;
        private static uint currentShader;
        private static GlModel currentModel;

        public readonly OpenGL Gl;

        public GlMesh Mesh;
        public GlTexture Texture;
        public GlShader Shader;
        public PrimitiveType RenderMode;


        public static bool IsCurrentShader(GlShader shader)
        {
            return currentShader == shader.Handle;
        }


        public GlModel(OpenGL gl, GlMesh mesh, GlTexture texture, GlShader shader)
        {
            Gl = gl;

            Mesh = mesh;
            Shader = shader;
            Texture = texture;
            ResetRenderMode();
        }

        public GlModel(OpenGL gl, GlMesh mesh, GlShader shader) : this(gl, mesh, gl.Core.Assets.GetTexture(""), shader)
        {
            
        }

        public GlModel(OpenGL gl, GlShader shader) : this(gl, gl.Core.Assets.GetGlMesh("sprite"), shader)
        {

        }

        public GlModel(OpenGL gl, GlTexture texture, GlShader shader) : this(gl, gl.Core.Assets.GetGlMesh("sprite"), texture, shader)
        {

        }


        public unsafe void ResetRenderMode()
        {
            RenderMode = Mesh.VertsPerPolygon switch
            {
                1 => PrimitiveType.Points,
                2 => PrimitiveType.Lines,
                3 => PrimitiveType.Triangles,
                4 => PrimitiveType.Quads, // does not work :(
                _ => PrimitiveType.Triangles,
            };
        }

        public unsafe void PrepareToDraw()
        {
            if (ReferenceEquals(this, currentModel)) return;
            currentModel = this;

            if (Mesh.VAOHandle != currentBuffer)
            {
                Gl.Api.BindVertexArray(Mesh.VAOHandle);
                currentBuffer = Mesh.VAOHandle;
            }

            if (Texture.Handle != currentTexture)
            {
                Gl.Api.BindTexture(TextureTarget.Texture2D, Texture.Handle);
                currentTexture = Texture.Handle;
            }

            if (Shader.Handle != currentShader)
            {
                Gl.Api.UseProgram(Shader.Handle);
                currentShader = Shader.Handle;
            }

            /*Gl.Api.BindVertexArray(VAOHandle);
            Gl.Api.BindTexture(TextureTarget.Texture2D, Texture.Handle);
            Gl.Api.UseProgram(Shader.Handle);*/
        }

        public unsafe void Draw()
        {
            if (Shader.Handle == 0) return;

            Shader.ApplyUniforms();

            Gl.Api.DrawElements(RenderMode, Mesh.VertexCount, DrawElementsType.UnsignedInt, (void*)null);

            //Gl.Core.Log(Gl.Api.GetError());
        }
    }
}
