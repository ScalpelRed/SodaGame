namespace Triode.Game.Animation
{
    public interface ITimeScale<T>
    {
        public T GetValue(float seconds);
    }
}
