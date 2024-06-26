﻿using static Triode.Game.Text.Ttf2mesh.Binding;

namespace Triode.Game.Text.Ttf2mesh
{
    public sealed class FontList : List<File>
    {
        internal readonly IntPtr Handle;

        internal FontList(IntPtr pointer)
        {
            Handle = pointer;

            IntPtr[] pointers = Util.UtilFunc.GetNullTerminatedArrayReferences(pointer);
            foreach (IntPtr p in pointers) Add(new File(p, false));
        }

        ~FontList()
        {
            ttf_free_list(Handle);
        }


    }
}
