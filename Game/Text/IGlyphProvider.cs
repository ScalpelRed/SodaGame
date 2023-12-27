using Game.Graphics;
using Game.OtherAssets;
using Game.Text.Ttf2mesh;

namespace Game.Text
{
    public interface IGlyphProvider
    {
        public GlMesh? GetGlMeshFor(char c);

        public RawMesh? GetRawMeshFor(char c);

        public Glyph? GetGlyphFor(char c);
    }
}
