using Game.ExactGame.SodaScreens;
using Game.Main;
using System.Numerics;
using Game.Graphics;
using Game.UI.Bounds;

namespace Game.ExactGame
{
    public class Bubble : ObjectModule
    {
        protected readonly SodaScreen SodaScreen;

        public volatile bool Active = false;

        protected UIBounds Bounds;
        protected ModelRenderer Renderer;


        public Bubble(WorldObject linkedObject, SodaScreen sodaScreen) : base(linkedObject, true)
        {
            SodaScreen = sodaScreen;
            Transform.Scale = Vector3.One * SodaScreen.Layer.BubbleScale;

            Bounds = new TransformBounds(linkedObject);
            Game.Core.Input.MouseMove += (Vector2 pos) => CheckPop();

            Renderer = new(linkedObject, sodaScreen.BubbleModel);
            Renderer.AssignValuesDictionary(sodaScreen.BubbleRendererValues);

            Active = true;
        }

        protected void CheckPop()
        {
            if (Active && Bounds.Contains(Game.MainCamera.ScreenToWorld(Game.Core.Input.MousePosition)))
            {
                SodaScreen.ItemBubble.Count += 1;
                SodaScreen.MakeBubbleInactive(this);
                //Game.Core.Audio.Play(sodaScreen.PopSound);
            }
        }

        public override void Step()
        {
            Transform.LocalPosition += SodaScreen.GetSpeedAt(Transform.LocalPosition.Y) * Vector3.UnitY * Game.DeltaTime;
            if (Transform.GlobalPosition.Y > SodaScreen.UpperBound) SodaScreen.MakeBubbleInactive(this);
        }

        public void Dispose()
        {
            Game.Core.Input.MouseMove -= (Vector2) => CheckPop();
        }
    }
}
