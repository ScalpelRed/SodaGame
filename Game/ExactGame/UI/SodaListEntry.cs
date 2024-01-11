using Game.ExactGame.SodaScenes;
using Game.Graphics;
using Game.Main;
using Game.UI;
using Game.UI.Bounds;
using Game.UI.Interactors;
using Silk.NET.Input;
using System.Numerics;

namespace Game.ExactGame.UI
{
    public sealed class SodaListEntry : UIModule
    {
        public const float Opacity = 0.5f;
        private readonly UIModelRenderer Background;

        public const float EntryHeight = 100;

        //private TextRenderer NameText;

        public SodaListEntry(WorldObject linkedObject, SodaScene soda) : base(linkedObject, false)
        {
            UITransform.SetAnchoringX(UITransform.AnchoringX.Stretch);
            UITransform.SetAnchoringY(UITransform.AnchoringY.Center);
            UITransform.MarginUp = EntryHeight;

            Background = new UIModelRenderer(linkedObject, Game.Core.Assets.Shaders.Get("meshSolid"));

            /*Multifont font = Game.Core.Assets.Multifonts.Get("default");
            NameText = new(new WorldObject(new Vector3(0, 0, 0), Game), new Util.ReferenceString("NAME"), font, TextHeight)
            {
                AlignmentX = 0,
            };*/

            new UIButton(new TransformBounds(linkedObject)).MouseUp += (MouseButton _) => Game.SetActiveSoda(soda);
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
