using System.Diagnostics.CodeAnalysis;

namespace Game.Animation.Interpolations
{
    public struct CubicInterpolation<T> : IInterpolation<T>
    {

        [NotNull] private dynamic MiddleFactorAD;
        [NotNull] private dynamic MiddleFactorBD;

        public T MiddleFactorA
        {
            get => MiddleFactorAD;
            set 
            {
                if (value is null) throw new ArgumentNullException(nameof(value));
                MiddleFactorAD = value; 
            }
        }
        public T MiddleFactorB
        {
            get => MiddleFactorBD;
            set
            {
                if (value is null) throw new ArgumentNullException(nameof(value));
                MiddleFactorBD = value;
            }
        }

        public CubicInterpolation(T middleFactorA, T middleFactorB)
        {
            MiddleFactorAD = null!; // если даже NotNull ему ни о чём не говорит, то я других способов не вижу
            MiddleFactorBD = null!;
            MiddleFactorA = middleFactorA;
            MiddleFactorB = middleFactorB;
        }

        public readonly T Interpolate(T a, T b, double time)
        {
            dynamic ad = a;
            dynamic bd = b;

            double timeN = 1 - time;
            double timeN2 = timeN * timeN;
            double time2 = time * time;
            return (T)((ad * timeN2 * timeN) + ((ad + MiddleFactorAD) * 3 * time * timeN2)  
                + ((bd + MiddleFactorBD) * 3 * timeN * time2) + bd * time2 * time);
        }
    }
}
