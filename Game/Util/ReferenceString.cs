using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Util
{

    /// <summary>
    /// This class should not be used the same way as default string.
    /// </summary>
    public class ReferenceString
    {
        public string String { get; set; }

        public ReferenceString(string value = "")
        {
            String = value;
        }

        public static bool operator ==(ReferenceString a, ReferenceString b) => a.ToString() == b.ToString();
        public static bool operator !=(ReferenceString a, ReferenceString b) => a.ToString() != b.ToString();
        public static string operator +(ReferenceString a, string b) => a.ToString() + b;
        public static string operator +(string a, ReferenceString b) => a + b.ToString();
        public static string operator +(ReferenceString a, ReferenceString b) => a.ToString() + b.ToString();

        public override string ToString()
        {
            return String;
        }

        public override bool Equals(object? obj)
        {
            if (obj is null) return false;
            if (obj is ReferenceString t) return String == t.ToString();
            return false;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }
    }
}
