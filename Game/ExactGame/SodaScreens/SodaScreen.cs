using Game.Audio;
using Game.Graphics;
using Game.Graphics.Renderers;
using Game.Main;
using Game.Phys;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.ExactGame.SodaScreens
{
    public abstract class SodaScreen : ObjectModule
    {
        protected List<Bubble> Bubbles = new();
        protected List<Bubble> InactiveBubbles = new();
        protected List<Bubble> ActiveSwitchQueue = new();

        public GlModel BubbleModel;
        public Sound PopSound;

        public readonly BubbleLayer Layer;
        public readonly float UpperBound;
        public readonly float LowerBound;
        public readonly float Height;
        public readonly float WidthHalf;
        protected Bubble LastBubble;

        public Dictionary<string, object> BubbleRendererValues = new();

        public Random Random;

        public SodaScreen(WorldObject linkedObject, BubbleLayer bubbleLayer) : base(linkedObject, false)
        {
            Layer = bubbleLayer;

            Height = Game.Core.OpenGL.ScreenSize.Y;
            UpperBound = Height * 0.5f + Layer.BubbleScale;
            LowerBound = Height * -0.5f - Layer.BubbleScale;
            WidthHalf = Game.Core.OpenGL.ScreenSize.X / 2;

            BubbleModel = new(Game.Core.OpenGL, Game.Core.Assets.GetTexture(""), Game.Core.Assets.GetShader(""));

            PopSound = Game.Core.Assets.GetSound("pop");

            Random = new Random(0);
            for (float y = LowerBound; y <= UpperBound; y += Layer.BubbleDistance) AddBubble(y);
            LastBubble = Bubbles[^1];
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

        public void MakeInactive(Bubble bubble)
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

            foreach (Bubble v in Bubbles)
            {
                v.LinkedObject.Step();
                int c = 0;
                foreach (Bubble b in Bubbles) if (b == v) c++;
                if (c != 1) Console.WriteLine(c);
            }

            if (LastBubble.Transform.GlobalPosition.Y - LowerBound >= Layer.BubbleDistance)
            {
                AddBubble(LowerBound);
            }
            if (!LastBubble.active) LastBubble.Step();
        }
    }
}
