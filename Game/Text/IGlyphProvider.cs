using Triode.Game.Graphics;
using Triode.Game.OtherAssets;
using Triode.Game.Text.Ttf2mesh;

namespace Triode.Game.Text
{
    public interface IGlyphProvider
    {
        public GlMesh? GetGlMeshFor(char c);

        public RawMesh? GetRawMeshFor(char c);

        public Glyph? GetGlyphFor(char c);
    }
}
