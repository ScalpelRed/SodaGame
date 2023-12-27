using Game.Graphics;
using Game.Main;

namespace Game.UI
{
    public class UIModelRenderer : UIRenderer
    {
        public GlModel Model;

        public UIModelRenderer(WorldObject linkedObject, GlShader shader) : this(linkedObject, new GlModel(linkedObject.Game.Core.OpenGL, shader))
        {

        }

        public UIModelRenderer(WorldObject linkedObject, GlModel model) : base(linkedObject)
        {
            Model = model;
        }

        public override void Draw(Camera camera)
        {
            Model.PrepareToDraw();

            Model.Shader.SetUniform("transform", UITransform.Matrix);
            Model.Shader.SetUniform("camera", camera.ViewMatrix);
            foreach (var value in Values) Model.Shader.SetUniform(value.Key, value.Value);

            Model.Draw();
        }
    }
}
