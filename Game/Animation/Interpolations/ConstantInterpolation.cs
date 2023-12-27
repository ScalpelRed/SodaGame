﻿namespace Game.Animation.Interpolations
{
    public struct ConstantInterpolation<T> : IInterpolation<T>
    {
        public readonly T Interpolate(T a, T b, double time) => a;
    }
}
