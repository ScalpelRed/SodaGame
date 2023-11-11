using Game.ExactGame.SodaScreens;
using Game.Graphics;
using Game.Main;
using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.ExactGame.SodaScenes
{
    public abstract class SodaScene : ObjectModule
    {
        public readonly List<SodaScreen> SodaScreens = new();
        protected ModelRenderer Background;

        public SodaScene(WorldObject linkedObject) : base(linkedObject, false)
        {
            Background = new ModelRenderer(new WorldObject(Vector3.Zero, Game), Game.Core.Assets.Shaders.Get(""));
            Background.Transform.LocalScale2 = Game.Core.OpenGL.ScreenSize;
        }

        public abstract Vector3 GetUIColor();

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
