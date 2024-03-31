using System.Runtime.InteropServices;
using static Triode.Game.Text.Ttf2mesh.Binding;

namespace Triode.Game.Text.Ttf2mesh
{
    public class Point
    {
        internal readonly IntPtr Handle;

        public float X { get; protected set; }
        public float Y { get; protected set; }
        public bool Spl { get; protected set; }
        public bool Onc { get; protected set; }
        public bool Shd { get; protected set; }
        public uint Res { get; protected set; }

        internal Point(IntPtr handle)
        {
            Handle = handle;
            ttf_point raw = Marshal.PtrToStructure<ttf_point>(handle);

            X = raw.x;
            Y = raw.y;
            Spl = raw.spl == 1;
            Onc = raw.onc == 1;
            Shd = raw.shd == 1;
            Res = raw.res;
        }
    }
}