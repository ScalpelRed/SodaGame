using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.ExactGame
{
    public struct BubbleLayer
    {
        public readonly float BubbleSpeed;
        public readonly float BubbleScale;
        public readonly float BubbleDistance;
        public readonly int Far;

        public BubbleLayer(int far, float bubbleSpeed, float bubbleScale, float bubbleDistance)
        {
            Far = far;
            BubbleSpeed = bubbleSpeed;
            BubbleScale = bubbleScale;
            BubbleDistance = bubbleDistance;
        }

        public static BubbleLayer Scale(BubbleLayer source, float scale, int far)
        {
            return new BubbleLayer(far, source.BubbleSpeed * scale, source.BubbleScale * scale, source.BubbleDistance * scale);
        }
    }
}
