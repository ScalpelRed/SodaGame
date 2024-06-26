﻿using Triode.Game.Assets;
using SoLoud;

namespace Triode.Game.Audio
{
    public class Sound
    {
        public Wav SoundData { get; protected set; }

        protected readonly AudioCore AudioCore;

        public unsafe Sound(string name, AudioCore audioCore)
        {
            AudioCore = audioCore;

            SoundData = new Wav();

            Stream stream;

            {
                Launcher.IO io = audioCore.GameCore.Platform.IO;
                string fullname = "sounds/" + name;
                if (io.ReadableExists(fullname + ".ogg")) stream = io.GetReadableStream(fullname + ".ogg");
                else if (io.ReadableExists(fullname + ".wav")) stream = io.GetReadableStream(fullname + ".wav");
                else if (io.ReadableExists(fullname + ".mp3")) stream = io.GetReadableStream(fullname + ".mp3");
                else throw new AssetNotFoundException("sound", name);
            }

            byte[] data = Util.UtilFunc.GetBytesFromStream(stream);
            stream.Close();

            fixed (byte* p = data)
            {
                IntPtr mem = (IntPtr)p;
                SoundData.loadMem(mem, (uint)data.Length, 1, 1);
            }
        }

    }
}
