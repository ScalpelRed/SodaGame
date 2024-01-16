using Game.Animation;
using Game.Animation.Interpolations;
using Game.ExactGame.Items;
using Game.Main;
using Game.Text;
using Game.UI;
using Game.Util;
using Silk.NET.Input;
using System.Numerics;

namespace Game.ExactGame.UI
{
    public sealed class Tabs : UIModule
    {
        public readonly BottomPanel BottomPanel;
        public readonly SodaSelection SodaSelection;
        public readonly UITextRenderer BubbleCountText;

        private readonly UITransform TabsTransform;

        public readonly float ScreenWidth;

        private readonly AnimationController MoveAnimator;
        private readonly InterpolationScale<float> AnimPos;

        public Tabs(WorldObject linkedObject) : base(linkedObject)
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

            BubbleCountText = new UITextRenderer(new WorldObject(Game, TabsTransform), Game.Fonts);
            BubbleCountText.UITransform.SetAnchoringX(UITransform.AnchoringX.Stretch);
            BubbleCountText.UITransform.SetAnchoringY(UITransform.AnchoringY.Up);
            BubbleCountText.AlignmentX = 0;
            BubbleCountText.AlignmentY = 1;
            Game.GetItemSlot<ItemBubble>()!.CountChanged += (float c) =>
            {
                BubbleCountText.StringText = c.ToString();
            };

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
            BubbleCountText.SetValue("color", new Vector4(color, 1));
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
            BubbleCountText.Step();
        }
    }
}
