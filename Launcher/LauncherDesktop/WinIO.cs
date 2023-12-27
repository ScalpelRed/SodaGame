using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triode.PlWin
{
    public class WinIO : IO
    {
        private string AssetDirectory = "../../../../../Assets/";

        public WinIO(WinPlatform platform)
        {
            /*try
            {
                AssetDirectory = GetAssetStreamReader("assetDirectory.ini", "").ReadToEnd();
            }
            catch (FileNotFoundException e)
            {
                _03310.Program.ReportException(e);
            }*/

            platform.Debug("Asset directory is " + AssetDirectory);
        }

        public override Stream GetWriteableStream(string name)
        {

            return new FileStream(AssetDirectory + name, FileMode.Open);
        }

        public override StreamReader GetWriteableStreamReader(string name)
        {
            return new StreamReader(AssetDirectory + name);
        }

        public override StreamWriter GetWriteableStreamWriter(string name)
        {
            return new StreamWriter(AssetDirectory + name);
        }

        public override bool WriteableExists(string name)
        {
            return File.Exists(AssetDirectory + name);
        }

        public override FileStream CreateWriteable(string name)
        {

            return new FileStream(AssetDirectory + name, FileMode.OpenOrCreate);
        }

        public override string[] ReadAllLinesWriteable(string name)
        {
            return File.ReadAllLines(AssetDirectory + name);
        }

        public override string ReadAllTextWriteable(string name)
        {
            return File.ReadAllText(AssetDirectory + name);
        }


        public override Stream GetReadableStream(string name)
        {
            return GetWriteableStream(name);
        }

        public override StreamReader GetReadableStreamReader(string name)
        {
            return GetWriteableStreamReader(name);
        }

        public override bool ReadableExists(string name)
        {
            return WriteableExists(name);
        }

        public override string[] ReadAllLinesReadable(string name)
        {
            return ReadAllLinesWriteable(name);
        }

        public override string ReadAllTextReadable(string name)
        {
            return ReadAllTextWriteable(name);
        }
    }
}
