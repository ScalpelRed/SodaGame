using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Triode.Game.Input
{
    public abstract class Mouse : InputDevice
    {
        public abstract Vector2 Position { get; protected set; }

        public event Action<Vector2>? MouseMove;

        public abstract int[] ButtonsDown { get; protected set; }

        public abstract bool IsButtonDown(int button);

        public event Action<int>? ButtonDown;
        public event Action<int>? ButtonUp;

        public const int BUTTON_INVALID = -1; // probably unnsecessary
        public const int BUTTON_LEFT = 0;
        public const int BUTTON_RIGHT = 1;
        public const int BUTTON_MIDDLE = 2;
    }
}
