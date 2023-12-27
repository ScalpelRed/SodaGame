using Game.Graphics;
using Game.Main;
using System.Numerics;

namespace Game.ExactGame.Items
{
    public class ItemBubble : Item
    {
        public ItemBubble(GameCore core) : base("item/bubble", core)
        {

        }

        public override WorldObject InstantiateDisplay()
        {
            ModelRenderer m = new(new WorldObject(Vector3.Zero, Core.Game),
                new GlModel(Core.OpenGL, Core.Assets.Textures.Get("soda/bubble"), Core.Assets.Shaders.Get("spriteTextured")));
            m.Transform.Scale2 = m.Model.Texture.Size;
            return m.LinkedObject;
        }
    }
}
