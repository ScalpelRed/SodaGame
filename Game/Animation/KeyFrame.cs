﻿using Game.Animation.Interpolations;

namespace Game.Animation
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
