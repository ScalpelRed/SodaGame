using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triode.Launcher
{
    public class NullIO : IO
    {
        public static readonly NullIO Instance = new();

        private static void Throw()
        {
            throw new IOException("This platform have no IO implemented.");
        }

        public override FileStream CreateWriteable(string name)
        {
            Throw();
            return null!;
        }

        public override Stream GetReadableStream(string name)
        {
            Throw();
            return null!;
        }

        public override StreamReader GetReadableStreamReader(string name)
        {
            Throw();
            return null!;
        }

        public override Stream GetWriteableStream(string name)
        {
            Throw();
            return null!;
        }

        public override StreamReader GetWriteableStreamReader(string name)
        {
            Throw();
            return null!;
        }

        public override StreamWriter GetWriteableStreamWriter(string name)
        {
            Throw();
            return null!;
        }

        public override bool ReadableExists(string name)
        {
            return false;
        }

        public override string[] ReadAllLinesReadable(string name)
        {
            Throw();
            return null!;
        }

        public override string[] ReadAllLinesWriteable(string name)
        {
            Throw();
            return null!;
        }

        public override string ReadAllTextReadable(string name)
        {
            Throw();
            return null!;
        }

        public override string ReadAllTextWriteable(string name)
        {
            Throw();
            return null!;
        }

        public override bool WriteableExists(string name)
        {
            return false;
        }
    }
}
