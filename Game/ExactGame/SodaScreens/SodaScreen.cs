using Game.Audio;
using Game.ExactGame.Items;
using Game.Graphics;
using Game.Main;
using System.Numerics;

namespace Game.ExactGame.SodaScreens
{
    public abstract class SodaScreen : ObjectModule
    {
        protected List<Bubble> Bubbles = [];
        protected List<Bubble> InactiveBubbles = [];
        protected List<Bubble> ActiveSwitchQueue = [];

        public GlModel BubbleModel;
        public Sound PopSound;

        public readonly BubbleLayer Layer;
        public readonly float UpperBound;
        public readonly float LowerBound;
        public readonly float Height;
        public readonly float WidthHalf;
        protected Bubble? LastBubble;

        public Dictionary<string, object> BubbleRendererValues = [];

        public Random Random;

        public ItemBubble ItemBubble;

        private bool Initialized;

        public SodaScreen(WorldObject linkedObject, BubbleLayer bubbleLayer) : base(linkedObject, false)
        {
            Layer = bubbleLayer;

            Height = Game.Core.OpenGL.ScreenSize.Y;
            UpperBound = Height * 0.5f + Layer.BubbleScale;
            LowerBound = Height * -0.5f - Layer.BubbleScale;
            WidthHalf = Game.Core.OpenGL.ScreenSize.X / 2;

            BubbleModel = new(Game.Core.OpenGL, Game.Core.Assets.Textures.Get(""), Game.Core.Assets.Shaders.Get(""));

            PopSound = Game.Core.Assets.Sounds.Get("pop");

            Random = new Random(0);

            ItemBubble = Game.GetItemSlot<ItemBubble>()!;

            Initialized = false;
        }

        public void SetActive()
        {
            if (Initialized)
            {
                foreach (Bubble v in Bubbles) v.Active = true;
            }
            else
            {
                for (float y = LowerBound; y <= UpperBound; y += Layer.BubbleDistance) AddBubble(y);
                LastBubble = Bubbles[^1];
                Initialized = true;
            }
        }

        public void SetInactive()
        {
            foreach (Bubble v in Bubbles) v.Active = false;
        }

        public void ClearBubbles() // If there's too much soda scenes in memory it's better to clear old ones
        {
            foreach (Bubble v in Bubbles) v.Dispose();
            LastBubble = null;
            Bubbles.Clear();
            InactiveBubbles.Clear();
            ActiveSwitchQueue.Clear();

            Initialized = false;
        }

        private Bubble AddBubble(float y)
        {
            Bubble bubble;
            if (InactiveBubbles.Count > 0)
            {
                bubble = InactiveBubbles[0];
                InactiveBubbles.RemoveAt(0);
                bubble.Transform.LocalPosition = new Vector3(GetNextX(), y, 0);
            }
            else
            {
                WorldObject t = new(new Vector3(GetNextX(), y, Layer.Far), Game, LinkedObject.Transform);
                bubble = new(t, this);
            }

            Bubbles.Add(bubble);
            bubble.Active = true;
            LastBubble = bubble;
            return bubble;
        }

        public void MakeBubbleInactive(Bubble bubble)
        {
            ActiveSwitchQueue.Add(bubble);
        }

        public float GetNextX()
        {
            return (float)((Random.NextDouble() * 2 - 1) * WidthHalf);
        }

        public float GetSpeedAt(float t)
        {
            float res = Height / (t + Height - LowerBound);
            return res * res * Layer.BubbleSpeed;
        }


        public override void Step()
        {
            foreach (Bubble bubble in ActiveSwitchQueue)
            {
                if (bubble.Active)
                {
                    bubble.Active = false;
                    Bubbles.Remove(bubble);
                    InactiveBubbles.Add(bubble);
                }
            }
            ActiveSwitchQueue.Clear();

            foreach (Bubble v in Bubbles) v.LinkedObject.Step();

            if (LastBubble!.Transform.GlobalPosition.Y - LowerBound >= Layer.BubbleDistance)
            {
                AddBubble(LowerBound);
            }
            if (!LastBubble.Active) LastBubble.Step();
        }
    }
}
