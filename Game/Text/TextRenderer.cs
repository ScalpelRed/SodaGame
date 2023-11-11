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

        protected List<Glyph?> CharGlyphs = new();
        protected List<GlMesh?> CharMeshes = new();

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

            for (int i = 0; i < text.Length; i++)
            {
                CharMeshes[i] = Font.GetGlMeshFor(text.String[i]);
                CharGlyphs[i] = Font.GetGlyphFor(text.String[i]);
            }
        }

        public override void Draw(Camera camera)
        {
            Model.Shader.SetUniform("transform", Transform.GlobalMatrix);
            Model.Shader.SetUniform("camera", camera.GetMatrix());
            Model.Shader.SetUniform("inColor", Color);

            Vector3 pos = Vector3.Zero;

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

                pos += Vector3.UnitX * CharGlyphs[i]!.Advance;
            }
        }
    }
}
