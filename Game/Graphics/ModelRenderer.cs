using Game.Main;


namespace Game.Graphics
{
    public class ModelRenderer : Renderer
    {
        public GlModel Model;

        public ModelRenderer(WorldObject linkedObject, GlShader shader) : this(linkedObject, new GlModel(linkedObject.Game.Core.OpenGL, shader))
        {

        }

        public ModelRenderer(WorldObject linkedObject, GlModel model) : base(linkedObject)
        {
            Model = model;
        }

        public override void Draw(Camera camera)
        {
            Model.Shader.SetUniform("transform", Transform.Matrix);
            Model.Shader.SetUniform("camera", camera.FinalMatrix);
            foreach (var value in Values) Model.Shader.SetUniform(value.Key, value.Value);

            Model.Draw();
        }
    }
}
