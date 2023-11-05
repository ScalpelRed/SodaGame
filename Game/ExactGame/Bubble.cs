using Game.ExactGame.SodaScreens;
using Game.Graphics.Renderers;
using Game.Main;
using Game.Phys;
using Game.UI;
using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Jitter.Collision.Shapes;

namespace Game.ExactGame
{
    public class Bubble : ObjectModule
    {
        protected readonly SodaScreen SodaScreen;

        public volatile bool active = false;
        public bool Active
        {
            get => active;
            set
            {
                active = value;
            }
        }

        protected TransformBounds Bounds;
        protected ModelRenderer Renderer;


        public Bubble(WorldObject linkedObject, SodaScreen sodaScreen) : base(linkedObject, true)
        {
            SodaScreen = sodaScreen;
            Transform.LocalScale = Vector3.One * SodaScreen.Layer.BubbleScale;

            Bounds = new(linkedObject);
            Game.Core.Input.MouseMove += (Vector2 pos) => CheckPop();

            Renderer = new(linkedObject, sodaScreen.BubbleModel);
            Renderer.AssignValuesDictionary(sodaScreen.BubbleRendererValues);

            Active = true;
        }

        protected void CheckPop()
        {
            if (Active && Bounds.Contains(Game.MainCamera.WorldToScreen(Game.Core.Input.MousePosition)))
            {
                SodaScreen.MakeInactive(this);
                //Game.Core.Audio.Play(sodaScreen.PopSound);
            }
        }

        public override void Step()
        {
            Transform.LocalPosition += SodaScreen.GetSpeedAt(Transform.LocalPosition.Y) * Vector3.UnitY * Game.DeltaTime;
            if (Transform.GlobalPosition.Y > SodaScreen.UpperBound) SodaScreen.MakeInactive(this);
        }
    }
}
