using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using static Game.Text.Ttf2mesh.Binding;

namespace Game.Text.Ttf2mesh
{
    public sealed class FontList : List<File>
    {
        internal readonly IntPtr Handle;

        internal FontList(IntPtr pointer)
        {
            Handle = pointer;

            pointer = Marshal.ReadIntPtr(pointer);

            IntPtr[] pointers = Util.UtilFunc.GetNullTerminatedArrayReferences<ttf_file>(pointer);

            foreach (IntPtr p in pointers) Add(new File(p, false));
        }

        ~FontList()
        {
            ttf_free_list(Handle);
        }


    }
}
