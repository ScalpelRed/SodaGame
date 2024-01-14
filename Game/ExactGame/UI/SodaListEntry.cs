using Game.ExactGame.SodaScenes;
using Game.Graphics;
using Game.Main;
using Game.Text;
using Game.UI;
using Game.UI.Bounds;
using Game.UI.Interactors;
using Silk.NET.Input;
using System.Numerics;
using Triode;

namespace Game.ExactGame.UI
{
    public sealed class SodaListEntry : UIModule
    {
        public const float Opacity = 0.5f;
        private readonly UIModelRenderer Background;

        public const float EntryHeight = 100;

        private readonly UITextRenderer NameText;
        private readonly UITextRenderer CommText;

        public SodaListEntry(WorldObject linkedObject, SodaScene soda) : base(linkedObject, false)
        {
            UITransform.SetAnchoringX(UITransform.AnchoringX.Stretch);
            UITransform.SetAnchoringY(UITransform.AnchoringY.Center);
            UITransform.MarginUp = EntryHeight;

            Background = new UIModelRenderer(linkedObject, Game.Core.Assets.Shaders.Get("meshSolid"));

            NameText = new(new WorldObject(Game, UITransform), Game.Fonts, soda.Info.Name)
            {
                Scale = 30
            };
            NameText.UITransform.SetAnchoringX(UITransform.AnchoringX.Left);
            NameText.UITransform.SetAnchoringY(UITransform.AnchoringY.Up);

            CommText = new(new WorldObject(Game, UITransform), Game.Fonts, soda.Info.Commentary)
            {
                Scale = 20
            };
            CommText.UITransform.SetAnchoringX(UITransform.AnchoringX.Left);
            CommText.UITransform.SetAnchoringY(UITransform.AnchoringY.Up);
            CommText.UITransform.AnchorRectCenter = Vector2.UnitY * 0.5f;

            new UIButton(new UITransformBounds(linkedObject)).MouseUp += (MouseButton _) => Game.SetActiveSoda(soda);
        }

        public void SetColor(Vector3 color)
        {
            Background.SetValue("color", new Vector4(color, Opacity));
            NameText.SetValue("color", new Vector4(Vector3.One - color, Opacity));
            CommText.SetValue("color", new Vector4(Vector3.One - color, Opacity * 0.8f));
        }

        public override void Step()
        {
            Background.Step();
            NameText.Step();
            CommText.Step();
        }
    }
}