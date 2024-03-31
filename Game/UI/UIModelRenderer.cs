using Triode.Game.Graphics;
using Triode.Game.General;

namespace Triode.Game.UI
{
    public class UIModelRenderer : Renderer
    {
        public GlModel Model;

        public UIModelRenderer(WorldObject linkedObject, GlShader shader) 
            : this(linkedObject, new GlModel(linkedObject.GameCore.OpenGL, shader))
        {

        }

        public UIModelRenderer(WorldObject linkedObject, GlModel model) : base(linkedObject)
        {
            Model = model;
        }

        public override void Draw(Camera camera)
        {
            Model.Shader.SetUniform("transform", Transform.FinalMatrix);
            Model.Shader.SetUniform("camera", camera.ViewMatrix);
            foreach (var value in Values) Model.Shader.SetUniform(value.Key, value.Value);

            Model.Draw();
        }

        protected override void Initialize()
        {
            
        }

        protected override void LinkWithObject()
        {
            
        }

        protected override void LinkWithTransform()
        {
            
        }

        protected override void UnlinkFromObject()
        {
            
        }

        protected override void UnlinkFromTransform()
        {
            
        }
    }
}
