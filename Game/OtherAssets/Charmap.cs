using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Game.Main;

namespace Game.OtherAssets
{
    public class Charmap
    {
        readonly SortedDictionary<char, (int, int)> CharBounds = new();

        public int CharCount
        {
            get => CharBounds.Count;
        }

        public readonly int CharHeight;
        public readonly int TotalWidth;

        public Charmap(string name, GameCore core)
        {
            char[] chars = core.Platform.IO.ReadAllTextReadable($"{name}.ini").ToCharArray();
            RawTexture tex = new(name, core);

            CharHeight = tex.SizeY;
            TotalWidth = tex.SizeX;

            int leftBound = 0;
            int rightBound = 0;

            foreach (char c in chars)
            {
                while (tex.GetPixel(rightBound, 0).X != 1) rightBound++;
                CharBounds.Add(c, (leftBound, rightBound));
                leftBound = rightBound + 1;
                rightBound = leftBound + 1;
            }
        }

        public (int left, int right) GetCharBounds(char c)
        {
            if (CharBounds.TryGetValue(c, out var bounds))
            {
                return bounds;
            }
            return CharBounds[' '];
        }

        public bool ContainsChar(char c)
        {
            return CharBounds.ContainsKey(c);
        }
    }
}
