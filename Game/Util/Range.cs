using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Util
{
    public struct Range<T> where T : IComparable<T>
    {
        private T value1;
        private T value2;

        public T Value1
        {
            readonly get => value1;
            set
            {
                value1 = value;
                if (value1.CompareTo(value2) > 0)
                {
                    Max = value1;
                    Min = value2;
                }
                else
                {
                    Max = value2;
                    Min = value1;
                }
            }
        }
        public T Value2
        {
            readonly get => value2;
            set
            {
                value2 = value;
                if (value2.CompareTo(value1) > 0)
                {
                    Max = value2;
                    Min = value1;
                }
                else
                {
                    Max = value1;
                    Min = value2;
                }
            }
        }

        public T Max { get; private set; }
        public T Min { get; private set; }


        public Range(T v1, T v2)
        {
            value1 = v1;
            value2 = v2;

            if (value1.CompareTo(value2) > 0)
            {
                Max = value1;
                Min = value2;
            }
            else
            {
                Max = value2;
                Min = value1;
            }
        }

        public Range(T val)
        {
            value1 = value2 = val;

            Max = value1;
            Min = value2;
        }

        public readonly T Clamp(T x)
        {
            if (x.CompareTo(Max) > 0) return Max;
            if (x.CompareTo(Min) < 0) return Min;
            return x;
        }


        public static bool operator ==(Range<T> a, Range<T> b) => a.Max.Equals(b.Max) && a.Min.Equals(b.Min);
        public static bool operator !=(Range<T> a, Range<T> b) => !a.Max.Equals(b.Max) || !a.Min.Equals(b.Min);

        public static bool operator ==(Range<T> a, T b) => b.CompareTo(a.Min) >= 0 && b.CompareTo(a.Max) <= 0;
        public static bool operator ==(T a, Range<T> b) => b == a;
            
        public static bool operator !=(Range<T> a, T b) => b.CompareTo(a.Min) < 0 || b.CompareTo(a.Max) > 0;
        public static bool operator !=(T a, Range<T> b) => b != a;

        public static bool operator >(Range<T> a, T b) => b.CompareTo(a.Max) > 0;
        public static bool operator >(T a, Range<T> b) => b > a;

        public static bool operator <(Range<T> a, T b) => b.CompareTo(a.Min) < 0;
        public static bool operator <(T a, Range<T> b) => b < a;

        public static bool operator >=(Range<T> a, T b) => b.CompareTo(a.Max) >= 0;
        public static bool operator >=(T a, Range<T> b) => b >= a;

        public static bool operator <=(Range<T> a, T b) => b.CompareTo(a.Min) <= 0;
        public static bool operator <=(T a, Range<T> b) => b <= a;

        public override readonly bool Equals(object? obj)
        {
            return (obj is Range<T> r) && (r == this);
        }

        public override readonly int GetHashCode()
        {
            return base.GetHashCode(); // idk
        }
    }
}
