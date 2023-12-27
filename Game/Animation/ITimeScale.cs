using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Animation
{
    public interface ITimeScale<T>
    {
        public T GetValue(float seconds);
    }
}
