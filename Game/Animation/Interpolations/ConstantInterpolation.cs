using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Animation.Interpolations
{
    public struct ConstantInterpolation<T> : IInterpolation<T>
    {
        public T Interpolate(T a, T b, double time)
        {
            return a;
        }
    }
}
