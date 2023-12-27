namespace Game.Animation.Interpolations
{
    public struct LinearFloatInterpolation : IInterpolation<float>
    {
        public readonly float Interpolate(float a, float b, float time)
        {
            return a + (b - a) * time;
        }
    }
}
