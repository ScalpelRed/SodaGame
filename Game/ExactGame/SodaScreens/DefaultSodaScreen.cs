using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.ExactGame.SodaScreens
{
    public class DefaultSodaScreen : SodaScreen
    {
        public DefaultSodaScreen(WorldObject linkedObject, Vector3 color, BubbleLayer bubbleLayer) : base(linkedObject, bubbleLayer)
        {
            BubbleModel.Shader = Game.Core.Assets.GetShader("meshTextured");
            BubbleModel.Texture = Game.Core.Assets.GetTexture("soda/bubble");
        }
    }
}
