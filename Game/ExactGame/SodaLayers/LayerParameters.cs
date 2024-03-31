namespace Triode.Game.ExactGame.SodaLayers
{
    public readonly struct LayerParameters
    {
        public readonly float BubbleSpeed;
        public readonly float BubbleScale;
        public readonly float BubbleDistance;
        public readonly int Index;

        public LayerParameters(int far, float bubbleSpeed, float bubbleScale, float bubbleDistance)
        {
            Index = far;
            BubbleSpeed = bubbleSpeed;
            BubbleScale = bubbleScale;
            BubbleDistance = bubbleDistance;
        }

        public static LayerParameters Scale(LayerParameters source, float scale, int index)
        {
            return new LayerParameters(index, source.BubbleSpeed * scale, source.BubbleScale * scale, source.BubbleDistance * scale);
        }
    }
}
