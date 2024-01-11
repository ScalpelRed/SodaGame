namespace Game.ExactGame.SodaLayers
{
    public readonly struct LayerParameters
    {
        public readonly float BubbleSpeed;
        public readonly float BubbleScale;
        public readonly float BubbleDistance;
        public readonly int Far;

        public LayerParameters(int far, float bubbleSpeed, float bubbleScale, float bubbleDistance)
        {
            Far = far;
            BubbleSpeed = bubbleSpeed;
            BubbleScale = bubbleScale;
            BubbleDistance = bubbleDistance;
        }

        public static LayerParameters Scale(LayerParameters source, float scale, int far)
        {
            return new LayerParameters(far, source.BubbleSpeed * scale, source.BubbleScale * scale, source.BubbleDistance * scale);
        }
    }
}
