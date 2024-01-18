using Game.Animation;
using Game.Animation.Interpolations;
using Game.Graphics;
using Game.Main;
using Game.UI;
using Game.UI.Bounds;
using Game.UI.Interactors;
using Game.Util;
using Silk.NET.Input;
using System.Numerics;
using static Game.UI.UITransform;

namespace Game.ExactGame.UI
{
    public sealed class BottomPanel : UIModule
    {
        private readonly float Opacity = 0.5f;
        private readonly UIModelRenderer Background;

        private readonly UIModelRenderer Highlighting;
        public readonly MouseInteractor Button1L;
        public readonly MouseInteractor Button00;
        public readonly MouseInteractor Button1R;
        private const float ButtonWidth = 1f / 3;

        public readonly OpenGL Gl;
        public readonly int Width;
        public const int Height = 100;

        private readonly AnimationController HighlightingAnim;
        private readonly InterpolationScale<float> HAnimPos;

        public BottomPanel(WorldObject linkedObject) : base(linkedObject)
        {
            Gl = Game.Core.OpenGL;
            Width = (int)Gl.ScreenSize.X;
            Transform.SetAnchoringX(AnchoringX.Stretch);
            Transform.SetAnchoringY(AnchoringY.Down);
            Transform.MarginUp = Height;

            Background = new UIModelRenderer(linkedObject, Game.Core.Assets.Shaders.Get("meshSolid"));

            Highlighting = new(CreateObjectForUI(Game, Transform), Game.Core.Assets.Shaders.Get("meshSolid"));
            Highlighting.Transform.SetAnchors(new Vector2(ButtonWidth * 1, 0), new Vector2(ButtonWidth * 2, 1));
            Highlighting.Transform.PosZ = 0.1f;

            Button1L = new UIButton(new AnyTransformBounds(CreateObjectForUI(Game, Transform)));
            Button1L.Transform.SetAnchors(new Vector2(0, 0), new Vector2(ButtonWidth, 1));
            Button1L.Transform.PosZ = 0.1f;
            Button1L.MouseUp += (MouseButton _) => MoveHighlighning(Button1L);

            Button00 = new UIButton(new AnyTransformBounds(CreateObjectForUI(Game, Transform)));
            Button00.Transform.SetAnchors(new Vector2(ButtonWidth, 0), new Vector2(ButtonWidth * 2, 1));
            Button00.Transform.PosZ = 0.1f;
            Button00.MouseUp += (MouseButton _) => MoveHighlighning(Button00);

            Button1R = new UIButton(new AnyTransformBounds(CreateObjectForUI(Game, Transform)));
            Button1R.Transform.SetAnchors(new Vector2(ButtonWidth * 2, 0), new Vector2(ButtonWidth * 3, 1));
            Button1R.Transform.PosZ = 0.1f;
            Button1R.MouseUp += (MouseButton _) => MoveHighlighning(Button1R);

            HAnimPos = new(0, 0, 0.1f, new LinearFloatInterpolation());
            HighlightingAnim = new(new Range<float>(0, HAnimPos.Duration),
                new SetterAnimator<float>(HAnimPos, (float v) =>
                {
                    Vector2 t = Highlighting.Transform.AnchorRectCenter;
                    t.X = v;
                    Highlighting.Transform.AnchorRectCenter = t;
                })
            );
        }
        
        public void SetColor(Vector3 color)
        {
            Background.SetValue("color", new Vector4(color, Opacity));
            Highlighting.SetValue("color", new Vector4(new Vector3(color.X + color.Y + color.Z) / 3, Opacity));
        }

        private void MoveHighlighning(MouseInteractor bt)
        {
            HAnimPos.Value1 = Highlighting.Transform.AnchorRectCenter.X;
            HAnimPos.Value2 = bt.Transform.AnchorRectCenter.X;
            HighlightingAnim.Time = 0;
            HighlightingAnim.Paused = false;
        }

        public override void Step()
        {
            Background.Step();
            HighlightingAnim.Update();
            Highlighting.Step();
        }
    }
}
