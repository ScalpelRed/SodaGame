namespace Triode.Game.Util
{
    public class ReferenceValue<T> // возможно, это костыль
    {
        private T value;

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                Changed?.Invoke(value);
            }
        }

        public ReferenceValue(T value)
        {
            this.value = value;
        }

        public event Action<T>? Changed;

        public static bool operator ==(ReferenceValue<T> a, ReferenceValue<T> b) => a.Value is not null && a.Value.Equals(b.Value);
        public static bool operator !=(ReferenceValue<T> a, ReferenceValue<T> b) => a.Value is null || a.Value.Equals(b.Value);

        public override string ToString()
        {
            return (value is not null) ? value.ToString() ?? "" : "";
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;

            if (obj is ReferenceValue<T> t)
            {
                if (value is null) return t.Value is null;
                else return value.Equals(t.Value);
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (value is not null) ? value.GetHashCode() : throw new NullReferenceException();
        }
    }
}
