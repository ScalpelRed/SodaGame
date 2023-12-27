using Game.Animation;
using Game.Main;
using Game.UI;
using System.Numerics;

namespace Game.ExactGame.UI
{
    public sealed class Tabs : UIModule
    {
        public readonly SodaSelection SodaSelection;
        public readonly BottomPanel BottomPanel;

        private Transform TabsTransform;

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

            SodaSelection = new SodaSelection(new WorldObject(Vector3.Zero, Game, TabsTransform));

            /*AnimPos = new(0, 0, 0.05, new LinearFloatInterpolation());
            MoveAnimator = new(new Range<double>(0, AnimPos.Duration),
                new SetterAnimator<float>(AnimPos, (float v) =>
                {
                    Vector3 t = TabsTransform.LocalPosition;
                    t.X = v;
                    TabsTransform.LocalPosition = t;
                })
            );*/

            //BottomPanel.Button1L.MouseUp += (MouseButton _) => MoveTab(-1);
            //BottomPanel.Button00.MouseUp += (MouseButton _) => MoveTab(0);
            //BottomPanel.Button1R.MouseUp += (MouseButton _) => MoveTab(1);
        }

        public void SetColor(Vector3 color)
        {
            BottomPanel.SetColor(color);
            //SodaSelection.SetColor(color);
        }

        private void MoveTab(int tab)
        {
            AnimPos.Value1 = TabsTransform.LocalPosition.X;
            AnimPos.Value2 = Game.Core.OpenGL.ScreenSize.X * tab;
            MoveAnimator.Progress = 0;
            MoveAnimator.Paused = false;
        }

        public override void Step()
        {
            //MoveAnimator.Update();


            BottomPanel.Step();
            SodaSelection.Step();
        }
    }
}
