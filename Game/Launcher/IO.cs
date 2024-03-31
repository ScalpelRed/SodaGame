using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triode.Launcher
{
    public abstract class IO
    {
        public abstract Stream GetReadableStream(string name);
        public abstract StreamReader GetReadableStreamReader(string name);
        public abstract bool ReadableExists(string name);
        public abstract string[] ReadAllLinesReadable(string name);
        public abstract string ReadAllTextReadable(string name);

        public abstract Stream GetWriteableStream(string name);
        public abstract StreamReader GetWriteableStreamReader(string name);
        public abstract bool WriteableExists(string name);
        public abstract string[] ReadAllLinesWriteable(string name);
        public abstract string ReadAllTextWriteable(string name);

        public abstract FileStream CreateWriteable(string name);
        public abstract StreamWriter GetWriteableStreamWriter(string name);
    }
}
