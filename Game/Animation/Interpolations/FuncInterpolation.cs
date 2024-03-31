namespace Triode.Game.Animation.Interpolations
{
    public struct FuncInterpolation<T> : IInterpolation<T>
    {
        public Func<T, T, float, T> Func;

        public  FuncInterpolation(Func<T, T, float, T> func)
        {
            Func = func;
        }

        public readonly T Interpolate(T a, T b, float time) => Func(a, b, time);
    }
}
