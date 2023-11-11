using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
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

		public static unsafe void CopyToManaged<T>(IntPtr source, T[] destination, int startIndex, int length)
		{
			// From: https://stackoverflow.com/questions/20981551/c-sharp-marshal-copy-intptr-to-16-bit-managed-unsigned-integer-array

			if (source == IntPtr.Zero) throw new ArgumentNullException(nameof(source));
			if (destination is null) throw new ArgumentNullException(nameof(destination));
			if (startIndex < 0) throw new ArgumentOutOfRangeException(nameof(startIndex));
			if (length < 0) throw new ArgumentOutOfRangeException(nameof(length));

			void* sourcePtr = (void*)source;
			Span<T> srcSpan = new(sourcePtr, length);
			Span<T> destSpan = new(destination, startIndex, length);

			srcSpan.CopyTo(destSpan);
		}

		public static unsafe string GetByteString(IntPtr source)
		{
			List<byte> str = new();
			for (IntPtr i = source; ; i = IntPtr.Add(i, sizeof(byte)))
			{
				byte c;
				try
				{
					c = *(byte*)i;
				}
				catch (NullReferenceException)
				{
					break;
				}
				if (c == 0) break;
				str.Add(c);
			}
			return System.Text.Encoding.UTF8.GetString(str.ToArray());
		}

		public static unsafe T[] GetNullTerminatedArray<T>(IntPtr pointerToFirst) where T : struct
		{
			List<T> str = new();
			while (pointerToFirst != IntPtr.Zero)
			{
				str.Add(Marshal.PtrToStructure<T>(pointerToFirst));
				pointerToFirst = Marshal.ReadIntPtr(pointerToFirst, Marshal.SizeOf<T>());
			}
			return str.ToArray();
		}

		public static unsafe IntPtr[] GetNullTerminatedArrayReferences<T>(IntPtr pointerToFirst) where T : struct
		{
			List<IntPtr> str = new();
			while (pointerToFirst != IntPtr.Zero)
			{
				str.Add(pointerToFirst);
				pointerToFirst = Marshal.ReadIntPtr(pointerToFirst, Marshal.SizeOf<T>());
			}
			return str.ToArray();
		}

		public static T[] ToLinear<T>(T[][] source)
        {
			IEnumerable<T> res = source[0].Concat(source[1]);
			for (int i = 2; i < source.Length; i++)
            {
				res = res.Concat(source[i]);
            }
			return res.ToArray();
        }
	}
}
