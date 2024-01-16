using Game.ExactGame.SodaLayers;
using Game.Main;
using System.Numerics;
using Game.Graphics;
using Game.UI.Bounds;
using Game.UI.Interactors;

namespace Game.ExactGame
{
    public class Bubble : ObjectModule
    {
        protected readonly SodaLayer SodaLayer;

        public volatile bool Active = false;

        protected MouseInteractor MouseInteractor;
        protected ModelRenderer Renderer;


        public Bubble(WorldObject linkedObject, SodaLayer sodaLayer) : base(linkedObject)
        {
            SodaLayer = sodaLayer;
            Transform.Scale = Vector3.One * SodaLayer.Layer.BubbleScale;

            MouseInteractor = new(new TransformBounds(linkedObject));
            MouseInteractor.MouseIn += (_) => TryPop();

            Renderer = new(linkedObject, sodaLayer.BubbleModel);
            Renderer.AssignValuesDictionary(sodaLayer.BubbleRendererValues);

            Active = true;
        }

        protected void TryPop()
        {
            if (Active)
            {
                SodaLayer.ItemBubble.Count += 1;
                SodaLayer.MakeBubbleInactive(this);
                //Game.Core.Audio.Play(sodaScreen.PopSound);
            }
        }

        /// <summary>
        /// Called from SodaLayer.ClearBubbles() in order to clear memory
        /// </summary>
        public void Dispose()
        {
            MouseInteractor.MouseIn -= (_) => TryPop();
        }

        public override void Step()
        {
            Transform.LocalPosition += SodaLayer.GetSpeedAt(Transform.LocalPosition.Y) * Vector3.UnitY * Game.DeltaTime;
            if (Transform.GlobalPosition.Y > SodaLayer.UpperBound) SodaLayer.MakeBubbleInactive(this);
            if (Active) Renderer.Step();
        }
    }
}
