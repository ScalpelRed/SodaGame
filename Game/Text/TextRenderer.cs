using Game.Graphics;
using Game.Main;
using Game.Text.Ttf2mesh;
using Game.Util;
using System.Numerics;

namespace Game.Text
{
    public class TextRenderer : Renderer
    {
        protected ReferenceString text;

        public ReferenceString Text
        {
            get => text;
            set
            {
                text.StringChanged -= (string prev, string _) => RefreshTextChange(prev);
                string prevText = text.String;
                text = value;
                text.StringChanged += (string prev, string _) => RefreshTextChange(prev);
                RefreshTextChange(prevText);
            }
        }

        protected IGlyphProvider font;

        public IGlyphProvider Font
        {
            get => font;
            set
            {
                font = value;
                RefreshTextGlyphs();
            }
        }

        protected float alignmentX;
        public float AlignmentX
        {
            get => alignmentX;
            set
            {
                alignmentX = value;
                RefreshAlignmentX();
            }
        }

        public float LineDistance = 1;

        protected List<Glyph?> CharGlyphs = [];
        protected List<GlMesh?> CharMeshes = [];
        protected List<float> LineAls = [];

        public Vector4 Color = Vector4.UnitW;

        public GlModel Model { get; protected set; }

        public TextRenderer(WorldObject linkedObject, ReferenceString text, IGlyphProvider font, float scale) : base(linkedObject)
        {
            this.text = text;
            text.StringChanged += (string prev, string _) => RefreshTextChange(prev);
            this.font = font;
            Model = new GlModel(Game.Core.OpenGL, Game.Core.Assets.Shaders.Get("textSolid"));

            RefreshTextChange("");
        }

        protected void RefreshTextGlyphs()
        {
            for (int i = 0; i < text.Length; i++)
            {
                CharMeshes[i] = Font.GetGlMeshFor(text.String[i]);
                CharGlyphs[i] = Font.GetGlyphFor(text.String[i]);
            }

            RefreshAlignmentX();
        }

        protected void RefreshTextChange(string prevText)
        {
            while (CharMeshes.Count < text.Length) CharMeshes.Add(null);
            while (CharGlyphs.Count < text.Length) CharGlyphs.Add(null);

            for (int i = 0; i < text.Length; i++)
            {
                if (i < prevText.Length && text.String[i] == prevText[i]) continue;

                CharMeshes[i] = Font.GetGlMeshFor(text.String[i]);
                CharGlyphs[i] = Font.GetGlyphFor(text.String[i]);
            }

            RefreshAlignmentX();
        }

        protected void RefreshAlignmentX()
        {
            LineAls.Clear();

            float lineAdvance = 0;
            int line = 0;
            int i = 0;
            foreach (Glyph? glyph in CharGlyphs)
            {
                if (glyph is not null) lineAdvance += glyph.Advance;
                else if (text.String[i] == '\n')
                {
                    LineAls.Add(0.5f * (alignmentX + 1) * lineAdvance);
                    lineAdvance = 0;
                    line++;
                }
                i++;
            }
            LineAls.Add(0.5f * (alignmentX + 1) * lineAdvance);
        }

        public Vector3 GetGlobalCharPos(int index)
        {
            Vector3 pos = -LineAls[0] * Vector3.UnitX;
            int line = 0;

            for (int i = 0; i < text.Length; i++)
            {
                Glyph? glyph = CharGlyphs[i];
                if (glyph is not null)
                {
                    if (i == index) break;
                    pos += Vector3.UnitX * glyph.Advance;
                }
                else if (text.String[i] == '\n')
                {
                    line++;
                    pos = -LineAls[line] * Vector3.UnitX - LineDistance * line * Vector3.UnitY;
                }
            }

            return Vector3.Transform(Vector3.Zero, Matrix4x4.CreateTranslation(pos) * Transform.Matrix);
        }

        public override void Draw(Camera camera)
        {
            Model.Shader.SetUniform("transform", Transform.Matrix);
            Model.Shader.SetUniform("camera", camera.FinalMatrix);
            Model.Shader.SetUniform("color", Color);

            Vector3 pos = -LineAls[0] * Vector3.UnitX;
            int line = 0;

            for (int i = 0; i < text.Length; i++)
            {
                GlMesh? mesh = CharMeshes[i];
                if (mesh is not null)
                {
                    Model.Shader.SetUniform("textransform", Matrix4x4.CreateTranslation(pos));
                    Model.Mesh = CharMeshes[i]!;
                    Model.PrepareToDraw();
                    Model.Draw();
                }

                Glyph? glyph = CharGlyphs[i];
                if (glyph is not null) pos += Vector3.UnitX * glyph.Advance;
                else if (text.String[i] == '\n')
                {
                    line++;
                    pos = -LineAls[line] * Vector3.UnitX - LineDistance * line * Vector3.UnitY;
                }
            }
        }
    }
}
