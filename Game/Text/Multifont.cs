using Triode.Game.Assets;
using Triode.Game.Graphics;
using Triode.Game.General;
using Triode.Game.OtherAssets;
using Triode.Game.Text.Ttf2mesh;

namespace Triode.Game.Text
{
    public class Multifont : IGlyphProvider
    {
        public readonly GameCore Core;
        protected Dictionary<char, Font?> CharLocations = new();
        protected List<Font> Fonts = new();

        public static Multifont FromFile(GameCore core, string name)
        {
            try
            {
                return new(core, core.Platform.IO.ReadAllLinesReadable($"fonts/{name}.mfont"));
            }
            catch (IOException)
            {
                throw new AssetNotFoundException("Multifont", name);
            }

        }

        public Multifont(GameCore core, params string[] fonts)
        {
            Core = core;
            foreach (string f in fonts) Fonts.Add(Core.Assets.Fonts.Get(f));
        }

        public Multifont(GameCore core, params Font[] fonts)
        {
            Core = core;
            foreach (Font f in fonts) Fonts.Add(f);
        }

        public bool AddFont(string name)
        {
            Font f = Core.Assets.Fonts.Get(name);
            if (Fonts.Contains(f)) return false;
            Fonts.Add(Core.Assets.Fonts.Get(name));
            return true;
        }

        public bool AddFont(Font font)
        {
            if (Fonts.Contains(font)) return false;
            Fonts.Add(font);
            return true;
        }

        public GlMesh? GetGlMeshFor(char c)
        {
            if (CharLocations.TryGetValue(c, out Font? font)) 
            {
                if (font is null) return null;
                return font.GetGlMeshFor(c); 
            }

            foreach (Font f in Fonts) 
            { 
                GlMesh? res = f.GetGlMeshFor(c);
                if (res is not null)
                {
                    CharLocations.Add(c, f);
                    return res;
                }
            }

            CharLocations.Add(c, null);
            return null;
        }

        public RawMesh? GetRawMeshFor(char c)
        {
            if (CharLocations.TryGetValue(c, out Font? font))
            {
                if (font is null) return null;
                return font.GetRawMeshFor(c);
            }

            foreach (Font f in Fonts)
            {
                RawMesh? res = f.GetRawMeshFor(c);
                if (res is not null)
                {
                    CharLocations.Add(c, f);
                    return res;
                }
            }

            CharLocations.Add(c, null);
            return null;
        }

        public Glyph? GetGlyphFor(char c)
        {
            if (CharLocations.TryGetValue(c, out Font? font))
            {
                if (font is null) return null;
                return font.GetGlyphFor(c);
            }

            foreach (Font f in Fonts)
            {
                Glyph? res = f.GetGlyphFor(c);
                if (res is not null)
                {
                    CharLocations.Add(c, f);
                    return res;
                }
            }

            CharLocations.Add(c, null);
            return null;
        }

        public bool ClearCharLocation(char c)
        {
            return CharLocations.Remove(c);
        }
    }
}
