using Silk.NET.OpenGL;

namespace Game.Graphics
{
    public class GlModel
    {
        public static uint currentVAO;
        private static uint currentTexture;
        private static uint currentShader;
        private static GlModel? currentModel;

        public readonly OpenGL Gl;

        protected GlMesh mesh;
        protected GlTexture texture;
        protected GlShader shader;

        public GlMesh Mesh
        {
            get => mesh;
            set
            {
                mesh = value;
                if (currentModel == this) currentModel = null;
            }
        }
        public GlTexture Texture
        {
            get => texture;
            set
            {
                texture = value;
                if (currentModel == this) currentModel = null;
            }
        }
        public GlShader Shader
        {
            get => shader;
            set
            {
                shader = value;
                if (currentModel == this) currentModel = null;
            }
        }

        public PrimitiveType RenderMode;


        public GlModel(OpenGL gl, GlMesh mesh, GlTexture texture, GlShader shader)
        {
            Gl = gl;

            this.mesh = mesh;
            this.shader = shader;
            this.texture = texture;
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
                4 => PrimitiveType.Quads, // does not work :(
                _ => PrimitiveType.Triangles,
            };
        }

        public unsafe void PrepareToDraw()
        {
            if (currentModel == this) return;
            currentModel = this;

            if (Mesh.VAOHandle != currentVAO)
            {
                Gl.Api.BindVertexArray(Mesh.VAOHandle);
                currentVAO = Mesh.VAOHandle;
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
