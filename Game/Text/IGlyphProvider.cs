using Game.Graphics;
using Game.OtherAssets;
using Game.Text.Ttf2mesh;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Text
{
    public interface IGlyphProvider
    {
        public GlMesh? GetGlMeshFor(char c);

        public RawMesh? GetRawMeshFor(char c);

        public Glyph? GetGlyphFor(char c);
    }
}
