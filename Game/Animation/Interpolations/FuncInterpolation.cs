﻿namespace Game.Animation.Interpolations
{
    public struct FuncInterpolation<T> : IInterpolation<T>
    {
        public Func<T, T, double, T> Func;

        public  FuncInterpolation(Func<T, T, double, T> func)
        {
            Func = func;
        }

        public readonly T Interpolate(T a, T b, double time) => Func(a, b, time);
    }
}