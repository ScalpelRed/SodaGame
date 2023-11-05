/*using Game.Graphics;
using Game.Main;
using Game.OtherAssets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Graphics.Renderers
{
    public class TextRenderer : Renderer
    {
        public GlModel CharModel;
        protected OpenGL Gl;

        private string text = "";
        public string Text
        {
            get => text;
            set
            {
                text = value.ToUpper();
                width = 0;

                CharBounds = new (int, int)[text.Length];
                int i = 0;
                foreach (char c in text)
                {
                    var cb = Charmap.GetCharBounds(c);
                    CharBounds[i] = cb;
                    width += (cb.right - cb.left) * Scale;
                    i++;
                }
            }
        }
        private (int left, int right)[] CharBounds;
        private readonly Charmap Charmap;

        public readonly float Scale;

        private float width;
        public float Width
        {
            get => width;
        }

        public TextRenderer(WorldObject linkedObject, string text, Charmap charmap, float scale = 1f) : base(linkedObject)
        {
            Gl = linkedObject.Game.Core.OpenGL;
            CharModel = new(Gl, Gl.Core.Assets.GetTexture("chars"), Gl.Core.Assets.GetShader("char"));
            CharBounds = Array.Empty<(int, int)>(); // Не обязательно
            Charmap = charmap;
            Text = text;
            Scale = scale;
        }

        public override void Draw()
        {
            CharModel.PrepareToDraw();

            CharModel.Shader.UniformMat4("transform", Transform.LocalMatrix);
            CharModel.Shader.UniformMat4("camera", Game.MainCamera.GetMatrix());
            CharModel.Shader.UniformInt("totalWidth", Charmap.TotalWidth);
            CharModel.Shader.UniformFloat("charHeight", Charmap.CharHeight * Scale);

            Vector2 localPos = Vector2.Zero;
            float prevWidth = 0;
            for (int i = 0; i < text.Length; i++)
            {
                var (leftBound, rightBound) = CharBounds[i];
                float charWidth = (rightBound - leftBound) * Scale;

                CharModel.Shader.UniformInt("leftBound", leftBound);
                CharModel.Shader.UniformInt("rightBound", rightBound);
                CharModel.Shader.UniformFloat("charWidth", charWidth);

                if (text[i] != '\n')
                {
                    localPos.X += prevWidth;
                    prevWidth = charWidth;

                    CharModel.Shader.UniformVec2("localPos", localPos);
                    CharModel.Draw();
                }
                else
                {
                    localPos.X = 0;
                    localPos.Y += Charmap.CharHeight * Scale;
                    prevWidth = 0;
                }
            }
        }


        public override void Step()
        {
            Draw();

        }
    }
}
*/