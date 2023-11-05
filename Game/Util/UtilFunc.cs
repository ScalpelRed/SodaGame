using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Jitter.LinearMath;

namespace Game.Util
{
    public static class UtilFunc
    {
        public static Vector2 XY(this Vector3 vec) => new(vec.X, vec.Y);

        public static Vector3 ToNumerics(this JVector vec) => new(vec.X, vec.Y, vec.Z);
        public static JVector ToJitter(this Vector3 vec) => new(vec.X, vec.Y, vec.Z);

        public static MemoryStream StreamToMemoryStream(Stream stream)
        {
            // InputStreamInvoker does not support Length field

            byte[] buffer = new byte[2048];
            MemoryStream ms = new();
            int read;
            while ((read = stream.Read(buffer, 0, buffer.Length)) > 0) ms.Write(buffer, 0, read);
            return ms;
        }

        public static byte[] GetBytesFromStream(Stream stream)
        {
            return StreamToMemoryStream(stream).ToArray();
        }

        public static void WriteMissingBytes(MemoryStream stream, int varBytes)
        {
            int byteCount = (int)(varBytes - stream.Length % varBytes);
            byte[] buffer = new byte[byteCount];
            stream.Write(buffer, 0, byteCount);
        }

        public static string[] SplitLines(string text)
        {
            return text.Split(new[] { '\r', '\n' });
        }

        public static bool Between(float a, float value, float b)
        {
            return (a <= b && a <= value && value <= b) || (b < a && b <= value && value <= a);
        }

        public static bool BetweenStrict(float a, float value, float b)
        {
            return (a < b && a < value && value < b) || (b < a && b < value && value < a);
        }
    }
}
