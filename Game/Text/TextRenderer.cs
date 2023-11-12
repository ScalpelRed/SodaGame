using Game.Graphics;
using Game.Main;
using Game.OtherAssets;
using Game.Text.Ttf2mesh;
using Game.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

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
                text = value;
                RefreshText();
            }
        }

        protected Font font;

        public Font Font
        {
            get => font;
            set
            {
                font = value;
                RefreshText();
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

        protected List<Glyph?> CharGlyphs = new();
        protected List<GlMesh?> CharMeshes = new();
        protected List<float> LineAls = new();

        public Vector4 Color = Vector4.UnitW;

        public GlModel Model { get; protected set; }

        public TextRenderer(WorldObject linkedObject, ReferenceString text, Font font) : base(linkedObject)
        {
            this.text = text;
            this.font = font;
            Model = new GlModel(Game.Core.OpenGL, Game.Core.Assets.Shaders.Get("textSolid"));

            text.StringChanged += (s) => RefreshText();

            RefreshText();
        }

        protected void RefreshText()
        {
            while (CharMeshes.Count < text.Length) CharMeshes.Add(null);
            while (CharGlyphs.Count < text.Length) CharGlyphs.Add(null);

            // TODO check if char didn't changed
            for (int i = 0; i < text.Length; i++)
            {
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

        public Vector3 GetCharPos(int index)
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

            return Vector3.Transform(Vector3.Zero, Matrix4x4.CreateTranslation(pos) * Transform.GlobalMatrix);
        }

        public override void Draw(Camera camera)
        {
            Model.Shader.SetUniform("transform", Transform.GlobalMatrix);
            Model.Shader.SetUniform("camera", camera.GetMatrix());
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
