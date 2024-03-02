using Game.Animation;
using Game.Animation.Interpolations;
using Game.ExactGame.Items;
using Game.Main;
using Game.Transforming;
using Game.UI;
using Game.Util;
using Silk.NET.Input;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace Game.ExactGame.UI
{
    public sealed class Tabs : ObjectModule
    {
        
        public UITransform NativeTransform { get; private set; } = null!;

        public BottomPanel BottomPanel { get; private set; } = null!;

        private List<IUITab> TabList = new();
        private UITransform TabsTransform = null!;

        public Tabs(WorldObject linkedObject) : base(linkedObject)
        {
            
        }

        protected override void Initialize()
        {
            BottomPanel = new BottomPanel(new WorldObject(new UITransform(GameCore, Transform), GameCore));
            BottomPanel.NativeTransform.PosZ = 0.1f;

            TabsTransform = new UITransform(GameCore)
            {
                Parent = Transform,
                PosZ = 0.1f
            };
            TabsTransform.SetAnchoringX(UITransform.AnchoringX.Stretch); // Unnecessary?
            TabsTransform.SetAnchoringY(UITransform.AnchoringY.Stretch);
        }

        protected override void LinkWithObject()
        {
            
        }

        protected override void LinkWithTransform()
        {
            NativeTransform = Transform.ValidateTransform<UITransform>(Transform);

            NativeTransform.SeveralChanges(() =>
            {
                NativeTransform.SetAnchoringX(UITransform.AnchoringX.Stretch);
                NativeTransform.SetAnchoringY(UITransform.AnchoringY.Stretch);
            });

            BottomPanel.Transform.Parent = Transform;

            TabsTransform.Parent = Transform;
        }

        protected override void UnlinkFromObject()
        {

        }

        protected override void UnlinkFromTransform()
        {

        }

        public override void Step()
        {
            BottomPanel.Step();
        }

        public void SetColor(Vector3 color)
        {
            BottomPanel.Color = color;
            //SodaSelection.SetColor(color);
            //BubbleCountText.SetValue("color", new Vector4(color, 1));
        }


        public void AddTab(IUITab tab, int index)
        {
            TabList.Insert(index, tab);
            BottomPanel.AddTab(tab, index);
        }

        public void RemoveTab(IUITab tab) 
        {
            TabList.Remove(tab);
            BottomPanel.RemoveTab(tab);
        }

        public void SetActiveTab(IUITab tab)
        {
            BottomPanel.SetActiveTab(tab);
        }

        /*public readonly SodaSelection SodaSelection;
        public readonly UITextRenderer BubbleCountText;

        

        public readonly float ScreenWidth;

        private readonly AnimationController MoveAnimator;
        private readonly InterpolationScale<float> AnimPos;

        public Tabs(WorldObject linkedObject) : base(linkedObject)
        {
            SodaSelection = new SodaSelection(UITransform.CreateObjectForUI(Game, TabsTransform));
            SodaSelection.Transform.AnchorRectCenter = new Vector2(-0.5f, 0.5f);

            BubbleCountText = new UITextRenderer(UITransform.CreateObjectForUI(Game, TabsTransform), Game.Fonts, Game.GetItem("bubble").Count.ToString());
            BubbleCountText.Transform.SetAnchoringX(UITransform.AnchoringX.Stretch);
            BubbleCountText.Transform.SetAnchoringY(UITransform.AnchoringY.Up);
            BubbleCountText.AlignmentX = 0;
            BubbleCountText.AlignmentY = 1;
            Game.GetItem("bubble").CountChanged += (float c) =>
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
        }*/
    }
}
