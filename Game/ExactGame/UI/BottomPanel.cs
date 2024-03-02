using Game.Animation;
using Game.Animation.Interpolations;
using Game.Graphics;
using Game.Main;
using Game.UI;
using Game.Transforming;
using Game.UI.Bounds;
using Game.UI.Interactors;
using Game.Util;
using Silk.NET.Input;
using System.Numerics;
using static Game.UI.UITransform;

namespace Game.ExactGame.UI
{
	public sealed class BottomPanel : ObjectModule
	{

		public UITransform NativeTransform { get; private set; } = null!;

		private UIModelRenderer Background = null!;

		public static float DefaultHeight = 100;

		public BottomPanel(WorldObject linkedObject) : base(linkedObject)
		{

		}

		protected override void Initialize()
		{
			Background = new UIModelRenderer(LinkedObject, GameCore.Assets.Shaders.Get("meshSolid"));
		}

		protected override void LinkWithObject()
		{

		}

		protected override void LinkWithTransform()
		{
			NativeTransform = Transform.ValidateTransform<UITransform>(Transform);

			NativeTransform.SeveralChanges(() =>
			{
				NativeTransform.SetAnchoringX(AnchoringX.Stretch);
				NativeTransform.SetAnchoringY(AnchoringY.Down);
				NativeTransform.MarginUp = DefaultHeight;
			});
		}

		protected override void UnlinkFromObject()
		{

		}

		protected override void UnlinkFromTransform()
		{

		}

		public override void Step()
		{
			Background.Step();
			
		}

		private Vector3 color;
		private float opacity = 0.5f;
		public float Opacity
		{
			get => opacity;
			set
			{
				opacity = value;
				UpdateColor();
			}
		}
		public Vector3 Color
		{
			get => color;
			set
			{
				color = value;
				UpdateColor();
			}
		}
		private void UpdateColor()
		{
			Background.SetValue("color", new Vector4(color, opacity));
			//Highlighting.SetValue("color", new Vector4(new Vector3(color.X + color.Y + color.Z) / 3, opacity));
		}

		private readonly List<TabButton> TabButtons = new();

		public void AddTab(IUITab tab, int index)
		{
            UITransform nativeTransform = new(GameCore, Transform);
			MouseInteractor button = new(new WorldObject(nativeTransform, GameCore));
            ScaleBasedRectBounds bounds = new(button.LinkedObject);

			button.RelatedBounds.Add(bounds);

			TabButtons.Insert(index, new TabButton(button, nativeTransform, tab));

			UpdateButtonWidths();
		}

		public void RemoveTab(IUITab tab)
		{
			for (int i = 0; i < TabButtons.Count; i++)
			{
				if (ReferenceEquals(TabButtons[i].Tab, tab))
				{
					TabButtons.RemoveAt(i);
					return;
				}
			}
		}

		private void UpdateButtonWidths()
		{
			if (TabButtons.Count >= 2)
			{
				float buttonWidth = 2f / (TabButtons.Count - 1);

				for (int i = 0; i < TabButtons.Count; i++)
				{
					TabButton button = TabButtons[i];

					button.NativeTransform.SetAnchors(new Vector2(buttonWidth * i, 0), new Vector2(buttonWidth * (i + 1), 1));
				}
			}
		}

		public void SetActiveTab(IUITab tab)
		{
			
		}

		private record TabButton(MouseInteractor Button, UITransform NativeTransform, IUITab Tab) { }


		/*

		private readonly UIModelRenderer Highlighting;
		public readonly MouseInteractor Button1L;
		public readonly MouseInteractor Button00;
		public readonly MouseInteractor Button1R;
		private const float ButtonWidth = 1f / 3;

		public readonly OpenGL Gl;

		private readonly AnimationController HighlightingAnim;
		private readonly InterpolationScale<float> HAnimPos;

		public BottomPanel(WorldObject linkedObject) : base(linkedObject)
		{
			Gl = GameCore.Core.OpenGL;

			Background = new UIModelRenderer(linkedObject, GameCore.Core.Assets.Shaders.Get("meshSolid"));

			Highlighting = new(CreateObjectForUI(GameCore, Transform), GameCore.Core.Assets.Shaders.Get("meshSolid"));
			Highlighting.Transform.SeveralChanges(() =>
			{
				Highlighting.Transform.SetAnchors(new Vector2(ButtonWidth * 1, 0), new Vector2(ButtonWidth * 2, 1));
				Highlighting.Transform.PosZ = 0.1f;
			});

			Button1L = new UIButton(new RectTransformBounds(CreateObjectForUI(GameCore, Transform)));
			Button1L.Transform.SetAnchors(new Vector2(0, 0), new Vector2(ButtonWidth, 1));
			Button1L.Transform.PosZ = 0.1f;
			Button1L.MouseUp += (MouseButton _) => MoveHighlighning(Button1L);

			Button00 = new UIButton(new RectTransformBounds(CreateObjectForUI(GameCore, Transform)));
			Button00.Transform.SetAnchors(new Vector2(ButtonWidth, 0), new Vector2(ButtonWidth * 2, 1));
			Button00.Transform.PosZ = 0.1f;
			Button00.MouseUp += (MouseButton _) => MoveHighlighning(Button00);

			Button1R = new UIButton(new RectTransformBounds(CreateObjectForUI(GameCore, Transform)));
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
		}*/
	}
}
