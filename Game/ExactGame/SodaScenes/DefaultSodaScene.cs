using Game.ExactGame.SodaScreens;
using Game.Main;
using System.Numerics;

namespace Game.ExactGame.SodaScenes
{
    public class DefaultSodaScene : SodaScene
    {
        protected Vector3 Color;

        public DefaultSodaScene(WorldObject linkedObject, Vector3 color) : base(linkedObject)
        {
            Color = color;

            foreach (BubbleLayer v in Game.Layers)
            {
                DefaultSodaScreen s = new(linkedObject, color, v);
                s.BubbleRendererValues.Add("tint", new Vector4(color, 1));
                SodaScreens.Add(s);
            }

            Background.Model.Shader = Game.Core.Assets.Shaders.Get("bubbleBackground");
            Background.SetValue("upColor", color);
            Background.SetValue("downColor", color * 0.2f);  
        }

        public override Vector3 GetUIColor()
        {
            return Color * 0.1f;
        }

        public override void Step()
        {
            base.Step();
        }
    }
}
