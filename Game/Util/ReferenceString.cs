using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Util
{
    public class ReferenceString
    {
        private string @string;

        public string String 
        {
            get => @string;
            set
            {
                string prev = @string;
                @string = value;
                StringChanged?.Invoke(prev, value);
            }
        }

        public ReferenceString(string value = "")
        {
            @string = value;
        }

        public int Length
        {
            get => @string.Length;
        }

        public event Action<string, string>? StringChanged;

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
