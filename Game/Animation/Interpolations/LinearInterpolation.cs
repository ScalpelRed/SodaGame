namespace Game.Animation.Interpolations
{
    public struct LinearInterpolation<T> : IInterpolation<T>
    {
        public readonly T Interpolate(T a, T b, double time)
        {
            dynamic ad = a;
            dynamic bd = b;
            return (T)(ad + (bd - ad) * (float)time);
        }
    }
}
