using Game.Graphics;
using Game.Main;
using Game.OtherAssets;
using Game.Text.Ttf2mesh;

namespace Game.Text
{
    public class Font
    {
        public readonly GameCore Core;

        protected readonly Ttf2mesh.File FontFile;

        public byte Quality = TTF2Mesh.QualityNormal;
        public int Features = TTF2Mesh.FeaturesDefault;

        public Font(string name, GameCore core)
        {
            Core = core;

            Stream stream;
            try
            {
                stream = core.Platform.IO.GetReadableStream("fonts/" + name + ".ttf");
            }
            catch (IOException)
            {
                throw new AssetNotFoundException("font", name);
            }

            Ttf2mesh.File fontfile = TTF2Mesh.LoadFromMem(Util.UtilFunc.GetBytesFromStream(stream), out TTF2Mesh.TTFResult res);
            //Ttf2mesh.File fontfile = TTF2Mesh.LoadFromFile(TTF2Mesh.ListSystemFonts(name)[0].FileName, out TTF2Mesh.TTFResult res);
            if (res != TTF2Mesh.TTFResult.Done) throw new Exception("Font wasn't loaded: " + res);
            FontFile = fontfile;
        }

        protected SortedDictionary<char, GlMesh?> CharGlMeshes = new();
        protected SortedDictionary<char, RawMesh?> CharRawMeshes = new();

        public GlMesh? GetGlMeshFor(char c)
        {
            if (CharGlMeshes.TryGetValue(c, out GlMesh? mesh)) return mesh!;

            // if no GlMesh for this character, trying to create it
            RawMesh? rawMesh = GetRawMeshFor(c);
            if (rawMesh is not null)
            {
                GlMesh glMesh = new(Core.OpenGL, rawMesh);
                CharGlMeshes.Add(c, glMesh);
                return glMesh;
            }

            // if no RawMesh for this character, there's nothing more we can do
            CharGlMeshes.Add(c, null);
            return null;
        }

        public RawMesh? GetRawMeshFor(char c)
        {
            if (CharRawMeshes.TryGetValue(c, out RawMesh? mesh)) return mesh!;

            // if no RawMesh for this character, trying to create it
            Glyph? glyph = FontFile.GetGlyph(c);
            if (glyph is not null)
            {
                Mesh? ttfmesh = glyph.ToMesh(Quality, Features, out TTF2Mesh.TTFResult _);
                if (ttfmesh is not null)
                {
                    RawMesh rawMesh = ttfmesh.ToRawMesh();
                    CharRawMeshes.Add(c, rawMesh);
                    return rawMesh;
                }
                else
                {
                    // char has no outline
                    CharRawMeshes.Add(c, null);
                    return null;
                }
            }

            // if no glyph for this character, there's nothing more we can do
            CharRawMeshes.Add(c, null);
            return null;
        }

        public Glyph? GetGlyphFor(char c)
        {
            return FontFile.GetGlyph(c);
        }
    }
}
