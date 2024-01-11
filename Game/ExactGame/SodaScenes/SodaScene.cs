using Game.ExactGame.SodaLayers;
using Game.Graphics;
using Game.Main;
using System.Numerics;

namespace Game.ExactGame.SodaScenes
{
    public abstract class SodaScene : ObjectModule
    {
        public readonly List<SodaLayer> SodaLayers = [];
        protected ModelRenderer Background;

        public abstract Vector3 UIColor { get; }

        public SodaScene(WorldObject linkedObject) : base(linkedObject, false)
        {
            Background = new ModelRenderer(new WorldObject(Vector3.Zero, Game), Game.Core.Assets.Shaders.Get(""));
            Background.Transform.Scale2 = Game.Core.OpenGL.ScreenSize;
        }

        public override void Step()
        {
            Background.Step();
            foreach (SodaLayer v in SodaLayers) v.Step();
        }

        public void SetActive()
        {
            foreach (SodaLayer v in SodaLayers) v.SetActive();
        }

        public void SetInactive()
        {
            foreach (SodaLayer v in SodaLayers) v.SetInactive();
        }

        public void ClearBubbles()
        {
            foreach (SodaLayer v in SodaLayers) v.ClearBubbles();
        }
    }
}
