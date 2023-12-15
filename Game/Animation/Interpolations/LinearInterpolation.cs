using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Animation.Interpolations
{
    public struct LinearInterpolation : IInterpolation<double>
    {
        public double Interpolate(double a, double b, double time) => a + (b - a) * time;
    }
}
