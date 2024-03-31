using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triode.Game.Input
{
    public abstract class Keyboard : InputDevice
    {
        public abstract int[] KeysDown { get; protected set; }

        public abstract bool IsKeyDown(int key);

        public event Action<int>? KeyDown;
        public event Action<int>? KeyUp;
    }
}
