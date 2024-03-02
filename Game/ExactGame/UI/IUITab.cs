using Game.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.ExactGame.UI
{
    public interface IUITab
    {
        public Vector3 Color { set; }
        public float Opacity { set; }
        public UITransform NativeTransform { get; }
    }
}
