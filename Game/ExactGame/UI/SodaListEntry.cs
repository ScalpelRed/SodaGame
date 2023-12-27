using Game.ExactGame.SodaScenes;
using Game.Graphics;
using Game.Main;
using Game.Text;
using Game.UI;
using Game.UI.Bounds;
using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.ExactGame.UI
{
    public sealed class SodaListEntry : ObjectModule
    {
        public const float Opacity = 0.5f;
        private readonly ModelRenderer Background;

        //private TextRenderer NameText;

        public SodaListEntry(WorldObject linkedObject, SodaScene soda) : base(linkedObject, false)
        {
            /*Background = new ModelRenderer(linkedObject, Game.Core.Assets.Shaders.Get("meshSolid"));
            Transform.UISize2 = new Vector2(Game.Core.OpenGL.ScreenSize.X, Game.Core.OpenGL.ScreenSize.Y * 0.1f);*/

            /*Multifont font = Game.Core.Assets.Multifonts.Get("default");
            NameText = new(new WorldObject(new Vector3(0, 0, 0), Game), new Util.ReferenceString("NAME"), font, TextHeight)
            {
                AlignmentX = 0,
            };*/

            //new UIButton(new TransformBounds(linkedObject)).MouseUp += (MouseButton _) => Game.SetActiveSoda(soda);
        }

        public void SetColor(Vector3 color)
        {
            Background.SetValue("color", new Vector4(color, Opacity));
        }

        public override void Step()
        {
            Background.Step();
            //NameText.Step();
        }
    }
}
