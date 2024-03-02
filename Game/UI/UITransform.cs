using Game.Graphics;
using Game.Main;
using Game.Transforming;
using Game.Util;
using System.Diagnostics;
using System.Numerics;

namespace Game.UI
{
    public class UITransform : Transform
	{
        // pivot = pivot
        // scale = anchors and margins
        // rotation = rotation
        // position = anchors
        // parent = parent

        private Vector3 pivot;
        private Matrix4x4 PivotMatrix = Matrix4x4.Identity;

        public Vector3 Pivot
        {
            get => pivot;
            set
            {
                pivot = value;
                PivotMatrix = Matrix4x4.CreateTranslation(-pivot);
                InvokeChanged();
            }
        }

        public Vector2 Pivot2
        {
            get => pivot.XY();
            set => Pivot = new Vector3(value, pivot.Z);
        }



        private float marginUp = 0;
        private float marginLeft = 0;
        private float marginDown = 0;
        private float marginRight = 0;

        public float MarginUp
        {
            get => marginUp;
            set
            {
                marginUp = value;
                InvokeChanged();
            }
        }

        public float MarginLeft
        {
            get => marginLeft;
            set
            {
                marginLeft = value;
                InvokeChanged();
            }
        }

        public float MarginDown
        {
            get => marginDown;
            set
            {
                marginDown = value;
                InvokeChanged();
            }
        }

        public float MarginRight
        {
            get => marginRight;
            set
            {
                marginRight = value;
                InvokeChanged();
            }
        }



        private Vector3 rotation = Vector3.Zero;

        private Matrix4x4 RotMatrix = Matrix4x4.Identity;

        public Vector3 Rotation
        {
            get => rotation;
            set
            {
                rotation = value;
                RotMatrix = Matrix4x4.CreateFromYawPitchRoll(value.X, value.Y, value.Z);
                InvokeChanged();
            }
        }

        public float RotationZ
        {
            get => rotation.Z;
            set => Rotation = new Vector3(Rotation.XY(), value);
        }


        private Vector2 anchor1 = Vector2.Zero;
        private Vector2 anchor2 = Vector2.Zero;

        public Vector2 Anchor1
        {
            get => anchor1;
            set
            {
                anchor1 = value;
                InvokeChanged();
            }
        }

        public Vector2 Anchor2
        {
            get => anchor2;
            set
            {
                anchor2 = value;
                InvokeChanged();
            }
        }

        private float posZ;
        public float PosZ
        {
            get => posZ;
            set
            {
                posZ = value;
                InvokeChanged();
            }
        }



        private Transform? parent;

        public Transform? Parent
        {
            get => parent;
            set
            {
                if (value == this) throw new ArgumentException("Transform cannot be a parent to itself.");

                if (HasParent) parent!.Changed -= InvokeChanged;
                parent = value;
                HasParent = value is not null;
                if (HasParent) parent!.Changed += InvokeChanged;
            }
        }

        public bool HasParent { get; private set; }

        
        private Vector2 bound1;
        private Vector2 bound2;

        public Vector2 Bound1
        {
            get
            {
                UpdateIfNecessary();
                return bound1;
            }
        }

        public Vector2 Bound2
        {
            get
            {
                UpdateIfNecessary();
                return bound2;
            }
        }

        private bool needsMatrixUpdate = false;
        public event Action? Changed;
        private void InvokeChanged()
        {
            if (severalChanges) return;
            needsMatrixUpdate = true;
            Changed?.Invoke();
        }

        private bool severalChanges = false;
        public void SeveralChanges(Action? changes)
        {
            severalChanges = true;
            changes?.Invoke();
            severalChanges = false;
            InvokeChanged();
        }


        private Matrix4x4 matrix;
        public Matrix4x4 FinalMatrix
        {
            get
            {
                UpdateIfNecessary();
                return matrix;
            }
        }

        private Matrix4x4 inheritableMatrix;
        public Matrix4x4 InheritableMatrix
        {
            get
            {
                UpdateIfNecessary();
                return inheritableMatrix;
            }
        }

        public void UpdateIfNecessary()
        {
            if (needsMatrixUpdate)
            {
                // TODO optimize, it processes all the values even if only one changed
                // may be done with bit mask to avoid using boolean array

                Vector2 pborder1;
                Vector2 pborder2;
                Matrix4x4 pmatrix;

                if (HasParent)
                {
                    if (parent is UITransform parentn)
                    {
                        pmatrix = parentn.InheritableMatrix;
                        pborder1 = parentn.Bound1;
                        pborder2 = parentn.Bound2;
                    }
                    else
                    {
                        pmatrix = parent!.InheritableMatrix;
                        pborder1 = Vector2.Zero;
                        pborder2 = Vector2.One;
                    }
                }
                else
                {
                    pborder2 = Gl.ScreenSize * 0.5f;
                    pborder1 = -pborder2;
                    pmatrix = Matrix4x4.Identity;
                }

                bound1 = pborder1 + (pborder2 - pborder1) * anchor1;
                bound1.X -= MarginLeft;
                bound1.Y -= MarginDown;

                bound2 = pborder1 + (pborder2 - pborder1) * anchor2;
                bound2.X += MarginRight;
                bound2.Y += MarginUp;

                inheritableMatrix = RotMatrix;
                inheritableMatrix *= Matrix4x4.CreateTranslation(new Vector3((bound1 - pborder1 + bound2 - pborder2) * 0.5f, posZ));
                inheritableMatrix *= pmatrix;

                Matrix4x4 scmat = Matrix4x4.CreateScale(new Vector3(bound2 - bound1, 1));
                matrix = PivotMatrix * scmat * inheritableMatrix;
                inheritableMatrix = Matrix4x4.CreateTranslation(Vector3.Transform(-pivot, scmat)) * inheritableMatrix;

                needsMatrixUpdate = false;
            }
        }

        public readonly OpenGL Gl;

        public UITransform(OpenGL gl, Transform? parent = null)
		{
            Gl = gl;
            Parent = parent;
            gl.Resized += InvokeChanged;
            needsMatrixUpdate = true;
		}

        public UITransform(GameCore gameCore, Transform? parent = null) : this(gameCore.OpenGL, parent)
        {

        }

        public void SetAnchoringX(AnchoringX anchoring)
		{
			switch (anchoring)
			{
				case AnchoringX.Right:
					anchor1 = new Vector2(1, anchor1.Y);
					anchor2 = new Vector2(1, anchor2.Y);
					break;
				case AnchoringX.Left:
                    anchor1 = new Vector2(0, anchor1.Y);
                    anchor2 = new Vector2(0, anchor2.Y);
                    break;
				case AnchoringX.Center:
                    anchor1 = new Vector2(0.5f, anchor1.Y);
                    anchor2 = new Vector2(0.5f, anchor2.Y);
                    break;
				case AnchoringX.Stretch:
                    anchor1 = new Vector2(0, anchor1.Y);
                    anchor2 = new Vector2(1, anchor2.Y);
                    break;
            }
            InvokeChanged();
		}

		public void SetAnchoringY(AnchoringY anchoring)
		{
            switch (anchoring)
            {
                case AnchoringY.Up:
                    anchor1 = new Vector2(anchor1.X, 1);
                    anchor2 = new Vector2(anchor2.X, 1);
                    break;
                case AnchoringY.Down:
                    anchor1 = new Vector2(anchor1.X, 0);
                    anchor2 = new Vector2(anchor2.X, 0);
                    break;
                case AnchoringY.Center:
                    anchor1 = new Vector2(anchor1.X, 0.5f);
                    anchor2 = new Vector2(anchor2.X, 0.5f);
                    break;
                case AnchoringY.Stretch:
                    anchor1 = new Vector2(anchor1.X, 0);
                    anchor2 = new Vector2(anchor2.X, 1);
                    break;
            }
            InvokeChanged();
        }

        public void SetAnchors(Vector2 anc1, Vector2 anc2)
        {
            anchor1 = anc1;
            anchor2 = anc2;
            InvokeChanged();
        }

        public void SetAnchors(Vector2 anc)
        {
            anchor1 = anc;
            anchor2 = anc;
            InvokeChanged();
        }

        public Vector2 MarginRectCenter
        {
            get => new Vector2(MarginRight - MarginLeft, MarginUp - MarginDown) * 0.5f;
            set
            {
                Vector2 delta = value - MarginRectCenter;

                marginRight += delta.X;
                marginLeft += delta.X;
                marginUp += delta.Y;
                marginDown += delta.Y;

                InvokeChanged();
            }
        }

        public float MarginRectUp
        {
            get => MarginUp;
            set
            {
                float delta = value - MarginRectUp;

                marginUp += delta;
                marginDown += delta;

                InvokeChanged();
            }
        }

        public float MarginRectDown
        {
            get => MarginDown;
            set
            {
                float delta = value - MarginRectDown;

                marginUp += delta;
                marginDown += delta;

                InvokeChanged();
            }
        }

        public float MarginRectLeft
        {
            get => MarginLeft;
            set
            {
                float delta = value - MarginRectLeft;

                marginLeft += delta;
                marginRight += delta;

                InvokeChanged();
            }
        }

        public float MarginRectRight
        {
            get => MarginRight;
            set
            {
                float delta = value - MarginRectRight;

                marginLeft += delta;
                marginRight += delta;

                InvokeChanged();
            }
        }

        public Vector2 AnchorRectCenter
        {
            get => (anchor2 + anchor1) * 0.5f;
            set
            {
                Vector2 delta = value - (anchor2 + anchor1) * 0.5f;

                anchor1 += delta;
                anchor2 += delta;

                InvokeChanged();
            }
        }

        public float AnchorRectUp
        {
            get => Anchor2.Y;
            set
            {
                float delta = value - AnchorRectUp;

                anchor1.Y += delta;
                anchor2.Y += delta;

                InvokeChanged();
            }
        }

        public float AnchorRectDown
        {
            get => Anchor1.Y;
            set
            {
                float delta = value - AnchorRectDown;

                anchor1.Y += delta;
                anchor2.Y += delta;

                InvokeChanged();
            }
        }

        public float AnchorRectLeft
        {
            get => Anchor1.X;
            set
            {
                float delta = value - AnchorRectLeft;

                anchor1.X += delta;
                anchor2.X += delta;

                InvokeChanged();
            }
        }

        public float AnchorRectRight
        {
            get => Anchor2.X;
            set
            {
                float delta = value - AnchorRectRight;

                anchor1.X += delta;
                anchor2.X += delta;

                InvokeChanged();
            }
        }

        public Vector2 ToNormalized(Vector2 vec)
        {
            return (vec - Bound1) / (bound2 - bound1);
        }

        public Vector2 FromNormalized(Vector2 vec)
        {
            return Bound1 + (bound2 - bound1) * vec;
        }

        public enum AnchoringX
		{
			Right,
			Left,
            Center,
			Stretch
		}

        public enum AnchoringY
        {
            Up,
            Down,
            Center,
            Stretch
        }
    }
}
