using Game.Main;
using System.Numerics;

namespace Game.ExactGame.SodaLayers
{
    public class DefaultSodaLayer : SodaLayer
    {
        public DefaultSodaLayer(WorldObject linkedObject, LayerParameters bubbleLayer) : base(linkedObject, bubbleLayer)
        {
            BubbleModel.Shader = Game.Core.Assets.Shaders.Get("meshTextured");
            BubbleModel.Texture = Game.Core.Assets.Textures.Get("soda/bubble");
        }
    }
}
