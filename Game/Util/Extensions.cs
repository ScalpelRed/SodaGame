using Jitter.LinearMath;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Triode.Game.Util
{
    public static class Extensions
    {
		public static Vector2 XY(this Vector3 vec) => new(vec.X, vec.Y);

		public static Vector3 ToNumerics(this JVector vec) => new(vec.X, vec.Y, vec.Z);
		public static JVector ToJitter(this Vector3 vec) => new(vec.X, vec.Y, vec.Z);

		public static string ToString2(this Matrix4x4 mat)
		{
			return
				$"{mat.M11} {mat.M21} {mat.M31} {mat.M41}\n" +
				$"{mat.M12} {mat.M22} {mat.M32} {mat.M42}\n" +
				$"{mat.M13} {mat.M23} {mat.M33} {mat.M43}\n" +
				$"{mat.M14} {mat.M24} {mat.M34} {mat.M44}";
		}

		public static V? GetByCloserLess<K, V>(this IDictionary<K, V> dict, K key) where K : IComparable<K>, IEquatable<K>
        {
			K? closerKey = dict.Keys.LastOrDefault(k => k.CompareTo(key) <= 0);
			if (closerKey is null || closerKey.Equals(default)) throw new KeyNotFoundException("No equal or less key found.");
			else return dict[closerKey];
        }

		public static bool TryGetByCloserLess<K, V>(this IDictionary<K, V> dict, K key, [MaybeNullWhen(false)] out V value) where K : IComparable<K>, IEquatable<K>
		{
			K? closerKey = dict.Keys.LastOrDefault(k => k.CompareTo(key) <= 0);
			if (closerKey is null || closerKey.Equals(default))
			{
				value = default;
				return false;
			}
			else return dict.TryGetValue(closerKey, out value);
		}

        public static V? GetByCloserGreater<K, V>(this IDictionary<K, V> dict, K key) where K : IComparable<K>, IEquatable<K>
		{
			K? closerKey = dict.Keys.LastOrDefault(k => k.CompareTo(key) >= 0);
			if (closerKey is null || closerKey.Equals(default)) throw new KeyNotFoundException("No equal or greater key found.");
			else return dict[closerKey];
		}

		public static bool TryGetByCloserGreater<K, V>(this IDictionary<K, V> dict, K key, [MaybeNullWhen(false)] out V value) where K : IComparable<K>, IEquatable<K>
		{
			K? closerKey = dict.Keys.LastOrDefault(k => k.CompareTo(key) >= 0);
			if (closerKey is null || closerKey.Equals(default))
			{
				value = default;
				return false;
			}
			else return dict.TryGetValue(closerKey, out value);
		}

		public static void Add<T>(this ICollection<T> coll, params T[] items)
		{
			foreach (T item in items) coll.Add(item);
		}
	}
}
