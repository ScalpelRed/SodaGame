using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Game.Main;


namespace Game.Graphics.Renderers
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

        public override void Draw()
        {
            Model.PrepareToDraw();

            Model.Shader.SetUniform("transform", Transform.GlobalMatrix);
            Model.Shader.SetUniform("camera", Game.MainCamera.GetMatrix());
            foreach (var value in Values) Model.Shader.SetUniform(value.Key, value.Value);

            Model.Draw();
        }
    }
}
