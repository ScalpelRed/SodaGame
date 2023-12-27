using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Animation
{
    public interface IAnimator
    {
        public void Apply(float seconds);
    }
}
