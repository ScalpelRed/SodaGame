using Triode.Game.Graphics;
using Triode.Game.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Triode.Game.ExactGame.Items
{
    /*public class ItemJelly : SpecialItem
    {
        protected Vector3 Color;

        public ItemJelly(string rawName, Vector3 color, GameCore core) : base(rawName, core)
        {
            Color = color;
        }

        public override WorldObject InstantiateIcon()
        {
            WorldObject res = new(Vector3.Zero, Core.Controller);
            ModelRenderer m = new(res, new GlModel(Core.OpenGL, Core.Assets.GlMeshes.Get("jellyCubeRigged"),
                Core.Assets.SolidTextures.Get(new Vector4(Color, 1)), Core.Assets.Shaders.Get("jellymeshTextured")));
            m.Transform.Scale *= 50f;
            _ = new Jelly(res);

            return res;
        }

        public override WorldObject InstantiateSodaObject()
        {
            return new WorldObject(Vector3.Zero, Core.Controller);
        }
    }*/
}
