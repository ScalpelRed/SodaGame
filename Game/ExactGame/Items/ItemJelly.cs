using Game.Graphics;
using Game.Main;
using System.Numerics;

namespace Game.ExactGame.Items
{
    public class ItemJelly : Item
    {
        protected Vector3 Color;

        public ItemJelly(string rawName, Vector3 color, GameCore core) : base(rawName, core)
        {
            Color = color;
        }

        public override WorldObject InstantiateDisplay()
        {
            ModelRenderer m = new(new WorldObject(Vector3.Zero, Core.Game), new GlModel(Core.OpenGL, Core.Assets.GlMeshes.Get("jellyCube"),
                Core.Assets.SolidTextures.Get(Vector4.One), Core.Assets.Shaders.Get("jellymeshTextured")));
            m.SetValue("tint", new Vector4(Color, 1));
            m.Transform.Scale *= 50f;
            new Jelly(m.LinkedObject);

            return m.LinkedObject;
        }
    }
}
