using Game.ExactGame.SodaLayers;
using Game.Main;
using System.Numerics;

namespace Game.ExactGame.SodaScenes
{
    public class DefaultSodaScene : SodaScene
    {
        public Vector3 Color { get; protected set; }
        public override Vector3 UIColor
        {
            get => Color * 0.1f;
        }
        

        public DefaultSodaScene(WorldObject linkedObject, Vector3 color) : base(linkedObject)
        {
            Color = color;

            foreach (LayerParameters v in Game.Layers)
            {
                DefaultSodaLayer s = new(linkedObject, v);
                s.BubbleRendererValues.Add("tint", new Vector4(color, 1));
                SodaLayers.Add(s);
            }

            Background.Model.Shader = Game.Core.Assets.Shaders.Get("bubbleBackground");
            Background.SetValue("upColor", color);
            Background.SetValue("downColor", color * 0.2f);  
        }

        public override void Step()
        {
            base.Step();
        }
    }
}
