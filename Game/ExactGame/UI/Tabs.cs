using Game.Animation;
using Game.Animation.Interpolations;
using Game.Main;
using Game.UI;
using Game.Util;
using Silk.NET.Input;
using System.Numerics;

namespace Game.ExactGame.UI
{
    public sealed class Tabs : UIModule
    {
        public readonly SodaSelection SodaSelection;
        public readonly BottomPanel BottomPanel;

        private UITransform TabsTransform;

        public readonly float ScreenWidth;

        private readonly AnimationController MoveAnimator;
        private readonly InterpolationScale<float> AnimPos;

        public Tabs(WorldObject linkedObject) : base(linkedObject, false)
        {
            UITransform.SetAnchoringX(UITransform.AnchoringX.Stretch);
            UITransform.SetAnchoringY(UITransform.AnchoringY.Stretch);

            BottomPanel = new BottomPanel(new WorldObject(Vector3.Zero, Game, Transform));
            BottomPanel.UITransform.Parent = UITransform;
            BottomPanel.UITransform.PosZ = 0.1f;

            TabsTransform = new UITransform(Game)
            {
                Parent = UITransform,
                PosZ = 0.1f
            };
            TabsTransform.SetAnchoringX(UITransform.AnchoringX.Stretch);
            TabsTransform.SetAnchoringY(UITransform.AnchoringY.Stretch);

            SodaSelection = new SodaSelection(new WorldObject(Vector3.Zero, Game));
            SodaSelection.UITransform.Parent = TabsTransform;
            SodaSelection.UITransform.AnchorRectCenter = new Vector2(-0.5f, 0.5f);

            AnimPos = new(0, 0, 0.05f, new LinearFloatInterpolation());
            MoveAnimator = new(new Range<float>(0, AnimPos.Duration),
                new SetterAnimator<float>(AnimPos, (float v) =>
                {
                    Vector2 t = TabsTransform.AnchorRectCenter;
                    t.X = v;
                    TabsTransform.AnchorRectCenter = t;
                })
            );

            BottomPanel.Button1L.MouseUp += (MouseButton _) => MoveTab(-1);
            BottomPanel.Button00.MouseUp += (MouseButton _) => MoveTab(0);
            BottomPanel.Button1R.MouseUp += (MouseButton _) => MoveTab(1);
        }

        public void SetColor(Vector3 color)
        {
            BottomPanel.SetColor(color);
            SodaSelection.SetColor(color);
        }

        private void MoveTab(int tab)
        {
            AnimPos.Value1 = TabsTransform.AnchorRectCenter.X;
            AnimPos.Value2 = -tab + 0.5f;
            MoveAnimator.Progress = 0;
            MoveAnimator.Paused = false;
        }

        public override void Step()
        {
            MoveAnimator.Update();


            BottomPanel.Step();
            SodaSelection.Step();
        }
    }
}
