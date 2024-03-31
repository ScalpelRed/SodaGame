using Triode.Game.Animation.Interpolations;

namespace Triode.Game.Animation
{
    public class InterpolationScale<T> : ITimeScale<T>
    {
        public IInterpolation<T> Interpolation;

        public InterpolationScale(T value1, T value2, float duration, IInterpolation<T> interpolation)
        {
            Value1 = value1;
            Value2 = value2;
            Duration = duration;
            Interpolation = interpolation;
        }

        public T Value1;
        public T Value2;
        public bool Extrapolate = false;
        public float Duration;

        public T GetValue(float seconds)
        {
            seconds /= Duration;
            if (!Extrapolate)
            {
                if (seconds > 1) seconds = 1;
                else if (seconds < 0) seconds = 0;
            }

            return Interpolation.Interpolate(Value1, Value2, seconds);
        }
    }
}
