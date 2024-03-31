using Triode.Game.Animation.Interpolations;

namespace Triode.Game.Animation
{
    public struct KeyFrame<T>
    {
        public T Value;
        public IInterpolation<T> OutInterpolation;

        public KeyFrame(T value, IInterpolation<T> outInterpolation)
        {
            Value = value;
            OutInterpolation = outInterpolation;
        }
    }
}
