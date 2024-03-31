namespace Triode.Game.Animation.Interpolations
{
    public struct ConstantInterpolation<T> : IInterpolation<T>
    {
        public readonly T Interpolate(T a, T b, float time) => a;
    }
}
