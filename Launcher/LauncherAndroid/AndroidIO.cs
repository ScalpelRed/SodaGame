using Android.Content.Res;

namespace Triode.PlAndroid
{
    public class AndroidIO : IO
    {
        private readonly AssetManager Assets;
        private readonly string WriteableDirectory = "";

        public AndroidIO(Activity activity)
        {
            WriteableDirectory = activity.ApplicationInfo.DataDir;
            Assets = activity.Assets;
        }

        public override Stream GetWriteableStream(string name)
        {
            return new FileStream(WriteableDirectory + "/" + name, FileMode.Open);
        }

        public override StreamReader GetWriteableStreamReader(string name)
        {
            return new StreamReader(GetWriteableStream(name));
        }

        public override StreamWriter GetWriteableStreamWriter(string name)
        {
            return new StreamWriter(GetWriteableStream(name));
        }

        public override bool WriteableExists(string name)
        {
            return File.Exists(WriteableDirectory + "/" + name);
        }

        public override FileStream CreateWriteable(string name)
        {
            return new FileStream(WriteableDirectory + "/" + name, FileMode.OpenOrCreate);
        }

        public override string[] ReadAllLinesWriteable(string name)
        {
            return File.ReadAllLines(WriteableDirectory + "/" + name);
        }

        public override string ReadAllTextWriteable(string name)
        {
            return File.ReadAllText(WriteableDirectory + "/" + name);
        }

        public override Stream GetReadableStream(string name)
        {
            return Assets.Open(name);
        }

        public override StreamReader GetReadableStreamReader(string name)
        {
            return new(Assets.Open(name));
        }

        public override bool ReadableExists(string name)
        {
            try
            {
                Assets.Open(name);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public override string[] ReadAllLinesReadable(string name)
        {
            List<string> res = new();
            StreamReader str = GetReadableStreamReader(name);

            while (!str.EndOfStream)
            {
                res.Add(str.ReadLine());
            }

            return res.ToArray();
        }

        public override string ReadAllTextReadable(string name)
        {
            return GetReadableStreamReader(name).ReadToEnd();
        }
    }
}
