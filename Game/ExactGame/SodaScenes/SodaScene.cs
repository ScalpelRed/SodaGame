using Game.ExactGame.SodaScreens;
using Game.Graphics;
using Game.Main;
using System.Numerics;

namespace Game.ExactGame.SodaScenes
{
    public abstract class SodaScene : ObjectModule
    {
        public readonly List<SodaScreen> SodaScreens = [];
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
            foreach (SodaScreen v in SodaScreens) v.Step();
        }

        public void SetActive()
        {
            foreach (SodaScreen v in SodaScreens) v.SetActive();
        }

        public void SetInactive()
        {
            foreach (SodaScreen v in SodaScreens) v.SetInactive();
        }

        public void ClearBubbles()
        {
            foreach (SodaScreen v in SodaScreens) v.ClearBubbles();
        }
    }
}
