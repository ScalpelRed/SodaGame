using System.Diagnostics.CodeAnalysis;

namespace Game.Animation.Interpolations
{
    public struct CubicFloatInterpolation : IInterpolation<float>
    {

        public float MiddleFactorA;
        public float MiddleFactorB;

        public CubicFloatInterpolation(float middleFactorA, float middleFactorB)
        {
            MiddleFactorA = middleFactorA;
            MiddleFactorB = middleFactorB;
        }

        public readonly float Interpolate(float a, float b, float time)
        {
            float timeN = 1 - time;
            float timeN2 = timeN * timeN;
            float time2 = time * time;
            return a * timeN2 * timeN + (a + MiddleFactorA) * 3 * time * timeN2 + (b + MiddleFactorB) * 3 * timeN * time2 + b * time2 * time;
        }
    }
}
