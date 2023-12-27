namespace Game.Animation
{
    public struct SetterAnimator<TValue> : IAnimator
    {
        public ITimeScale<TValue> ValueProvider;

        private readonly Action<TValue> Setter;

        public SetterAnimator(ITimeScale<TValue> valueProvider, Action<TValue> setter)
        {
            Setter = setter;
            ValueProvider = valueProvider;
        }

        public readonly void Apply(float seconds)
        {
            Setter(ValueProvider.GetValue(seconds));
        }
    }
}
