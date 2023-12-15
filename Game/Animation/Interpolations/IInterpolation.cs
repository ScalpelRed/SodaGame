using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Animation.Interpolations
{
    public interface IInterpolation<T>
    {
        public T Interpolate(T a, T b, double time);
    }
}
