using Game.Graphics;
using Game.Graphics.Renderers;
using Game.Main;
using Game.UI;
using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.ExactGame.UI
{
    public sealed class BottomPanel : ObjectGroup
    {
        private readonly float Opacity = 0.5f;
        private readonly ModelRenderer Background;

        /*private readonly ModelRenderer ButtonSeparator1;
        private readonly ModelRenderer ButtonSeparator2;
        private readonly float SeparatorDarkness = 0.5f;*/

        private readonly ModelRenderer Highlighting;
        private readonly UIButton Button1;
        private readonly UIButton Button2;
        private readonly UIButton Button3;
        private readonly float ButtonWidth = 1f / 3;

        public readonly OpenGL Gl;
        private readonly int Width;
        private readonly int Height;

        public BottomPanel(WorldObject linkedObject) : base(linkedObject)
        {
            Gl = Game.Core.OpenGL;
            Width = (int)Gl.ScreenSize.X;
            Height = 100;
            Transform.LocalScale2 = new Vector2(Width, Height);
            Transform.LocalPosition2 = Vector2.UnitY * (-Gl.ScreenSize.Y + Height) * 0.5f;
            Background = new ModelRenderer(linkedObject, Game.Core.Assets.GetShader("uiBackground"));

            Highlighting = new ModelRenderer(new WorldObject(Vector3.Zero, Game, Transform), Game.Core.Assets.GetShader("uiBackground"));
            Highlighting.Transform.LocalScale2 = new Vector2(ButtonWidth, 1);

            Button1 = new UIButton(new TransformBounds(new WorldObject(Vector3.UnitX * ButtonWidth * 1, Game, Transform)));
            Button1.Transform.LocalScale2 = new Vector2(ButtonWidth, 1);
            Button1.MouseUp += (MouseButton _) => Highlighting.Transform.LocalPosition2 = Button1.Transform.LocalPosition2;

            Button2 = new UIButton(new TransformBounds(new WorldObject(Vector3.UnitX * ButtonWidth * 0, Game, Transform)));
            Button2.Transform.LocalScale2 = new Vector2(ButtonWidth, 1);
            Button2.MouseUp += (MouseButton _) => Highlighting.Transform.LocalPosition2 = Button2.Transform.LocalPosition2;

            Button3 = new UIButton(new TransformBounds(new WorldObject(Vector3.UnitX * ButtonWidth * -1, Game, Transform)));
            Button3.Transform.LocalScale2 = new Vector2(ButtonWidth, 1);
            Button3.MouseUp += (MouseButton _) => Highlighting.Transform.LocalPosition2 = Button3.Transform.LocalPosition2;

            /*ButtonSeparator1 = new ModelRenderer(new WorldObject(new Vector3(-0.33f * 0.5f, 0, 0), Game, Transform), Game.Core.Assets.GetShader("uiBackground"));
            ButtonSeparator1.Transform.LocalScale2 = new Vector2(0.01f, 0.8f);

            ButtonSeparator2 = new ModelRenderer(new WorldObject(new Vector3(0.33f * 0.5f, 0, 0), Game, Transform), Game.Core.Assets.GetShader("uiBackground"));
            ButtonSeparator2.Transform.LocalScale2 = new Vector2(0.01f, 0.8f);*/
        }

        public void SetColor(Vector3 color)
        {
            Background.SetValue("inColor", new Vector4(color, Opacity));
            Highlighting.SetValue("inColor", new Vector4(new Vector3(color.X + color.Y + color.Z) / 3, Opacity));
            /*ButtonSeparator1.SetValue("inColor", new Vector4(color * SeparatorDarkness, Opacity));
            ButtonSeparator2.SetValue("inColor", new Vector4(color * SeparatorDarkness, Opacity));*/
        }

        public override void Step()
        {
            Background.Step();
            Highlighting.Step();
            /*ButtonSeparator1.Step();
            ButtonSeparator2.Step();*/
        }
    }
}
