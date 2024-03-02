using Game.Graphics;
using Game.Main;
using Game.Text;
using Game.Text.Ttf2mesh;
using Game.Util;
using System.Numerics;
using static Game.UI.UITransform;

namespace Game.UI
{
    /*public class UITextRenderer : UIRenderer
    {
        private ReferenceValue<string> text;
        public ReferenceValue<string> Text
        {
            get => text;
            set
            {
                text.Changed -= (str) => NeedsUpdate = true;
                text = value;
                text.Changed += (str) => NeedsUpdate = true;
                NeedsUpdate = true;
            }
        }
        public string StringText
        {
            get => text.Value;
            set => text.Value = value;
        }

        private IGlyphProvider font;
        public IGlyphProvider Font
        {
            get => font;
            set
            {
                font = value;
                NeedsUpdate = true;
            }
        }

        private float scale = 50;
        public float Scale
        {
            get => scale;
            set
            {
                scale = value;
                NeedsUpdate = true;
            }
        }

        private float lineHeight = 1;
        public float LineHeight
        {
            get => lineHeight;
            set
            {
                lineHeight = value;
                NeedsUpdate = true;
            }
        }

        private float alignmentX = -1;
        public float AlignmentX
        {
            get => alignmentX;
            set
            {
                alignmentX = value;
                NeedsUpdate = true;
            }
        }

        private float alignmentY = 1;
        public float AlignmentY
        {
            get => alignmentY;
            set
            {
                alignmentY = value;
                NeedsUpdate = true;
            }
        }

        public GlModel Model { get; protected set; }

        private float VerticalAlignment = 0f;
        private readonly List<Vector2> LineAlignments = [];
        private readonly List<Char> CharData = [];

        public UITextRenderer(WorldObject linkedObject, IGlyphProvider font, ReferenceValue<string> text) : base(linkedObject)
        {
            this.text = text;
            this.font = font;
            Model = new GlModel(GameCore.Core.OpenGL, GameCore.Core.Assets.Shaders.Get("textSolid"));

            text.Changed += (_) => NeedsUpdate = true;
            Transform.Changed += () => NeedsUpdate = true;

            SetValue("color", Vector4.UnitW);

            NeedsUpdate = true;
        }

        public UITextRenderer(WorldObject linkedObject, IGlyphProvider font, string text = "")
            : this(linkedObject, font, new ReferenceValue<string>(text))
        {

        }

        private bool NeedsUpdate = true;

        private void UpdateIfNecessary()
        {
            if (NeedsUpdate)
            {
                CharData.Clear();
                LineAlignments.Clear();

                float cposX = 0;
                int line = 0;

                float edgeX = (Transform.Bound2.X - Transform.Bound1.X) * 0.5f * alignmentX;

                for (int i = 0; i < Text.Value.Length; i++)
                {
                    char c = Text.Value[i];
                    Glyph? glyph = Font.GetGlyphFor(c);
                    GlMesh? mesh = Font.GetGlMeshFor(c);

                    CharData.Add(new Char(c, glyph, mesh, cposX, line));

                    if (glyph is not null)
                    {
                        cposX += glyph.Advance * scale;
                    }

                    if (c == '\n')
                    {
                        cposX = 0;
                        line++;
                        LineAlignments.Add(new Vector2(edgeX - cposX * (alignmentX + 1) * 0.5f, -lineHeight * scale * line));
                    }
                }

                line++;
                float totalHeight = -lineHeight * scale * line;
                LineAlignments.Add(new Vector2(edgeX - cposX * (alignmentX + 1) * 0.5f, totalHeight));

                // Not simplified: VerticalAlignment = (UITransform.Bound2.Y - UITransform.Bound1.Y) * 0.5f * alignmentY - totalHeight * (-alignmentY + 1) * 0.5f;
                VerticalAlignment = ((Transform.Bound2.Y - Transform.Bound1.Y + totalHeight) * alignmentY - totalHeight) * 0.5f;

                NeedsUpdate = false;
            }
        }

        public override void Draw(Camera camera)
        {
            UpdateIfNecessary();

            Model.Shader.SetUniform("transform", Transform.InheritableMatrix);
            Model.Shader.SetUniform("camera", camera.ViewMatrix);
            foreach (var value in Values) Model.Shader.SetUniform(value.Key, value.Value);

            Matrix4x4 texTransform = Matrix4x4.CreateScale(scale);
            foreach (Char c in CharData)
            {
                if (c.Mesh is not null)
                {
                    Model.Mesh = c.Mesh;

                    Vector2 lineAl = LineAlignments[c.Line];
                    texTransform.M41 = c.PosX + lineAl.X;
                    texTransform.M42 = lineAl.Y + VerticalAlignment;

                    Model.Shader.SetUniform("textransform", texTransform);
                    Model.Draw();
                }
            }
        }

        private record Char(char Character, Glyph? Glyph, GlMesh? Mesh, float PosX, int Line);
    }*/
}
