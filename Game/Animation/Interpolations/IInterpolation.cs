namespace Game.Animation.Interpolations
{
    public interface IInterpolation<T>
    {
        public T Interpolate(T a, T b, double time);
    }
}
