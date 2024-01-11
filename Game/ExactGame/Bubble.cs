using Game.ExactGame.SodaLayers;
using Game.Main;
using System.Numerics;
using Game.Graphics;
using Game.UI.Bounds;

namespace Game.ExactGame
{
    public class Bubble : ObjectModule
    {
        protected readonly SodaLayer SodaLayer;

        public volatile bool Active = false;

        protected UIBounds Bounds;
        protected ModelRenderer Renderer;


        public Bubble(WorldObject linkedObject, SodaLayer sodaLayer) : base(linkedObject, true)
        {
            SodaLayer = sodaLayer;
            Transform.Scale = Vector3.One * SodaLayer.Layer.BubbleScale;

            Bounds = new TransformBounds(linkedObject);
            Game.Core.Input.MouseMove += (Vector2 pos) => CheckPop();

            Renderer = new(linkedObject, sodaLayer.BubbleModel);
            Renderer.AssignValuesDictionary(sodaLayer.BubbleRendererValues);

            Active = true;
        }

        protected void CheckPop()
        {
            if (Active && Bounds.Contains(Game.MainCamera.WorldToScreen(Game.Core.Input.MousePosition)))
            {
                SodaLayer.ItemBubble.Count += 1;
                SodaLayer.MakeBubbleInactive(this);
                //Game.Core.Audio.Play(sodaScreen.PopSound);
            }
        }

        public override void Step()
        {
            Transform.LocalPosition += SodaLayer.GetSpeedAt(Transform.LocalPosition.Y) * Vector3.UnitY * Game.DeltaTime;
            if (Transform.GlobalPosition.Y > SodaLayer.UpperBound) SodaLayer.MakeBubbleInactive(this);
        }

        public void Dispose()
        {
            Game.Core.Input.MouseMove -= (Vector2) => CheckPop();
        }
    }
}
