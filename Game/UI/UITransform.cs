using Game.Main;
using Game.Util;
using System.Numerics;

namespace Game.UI
{
    public class UITransform : ObjectModule
	{
        // pivot = pivot
        // scale = anchors and margins
        // rotation = rotation
        // position = anchors
        // === parent = parent

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



        /*private Vector3 rotation = Vector3.Zero;

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
        }*/



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



        private UITransform? parent;

        public UITransform? Parent
        {
            get => parent;
            set
            {
                if (HasParent) parent!.Changed -= InvokeChanged;
                parent = value;
                HasParent = value is not null;
                if (HasParent) parent!.Changed += InvokeChanged;
            }
        }

        public bool HasParent { get; private set; }


        private Vector2 bound1;
        private Vector2 bound2;
        private Matrix4x4 InheritableMatrix;
        protected void GetInheritedValues(out Vector2 border1, out Vector2 border2, out Matrix4x4 inhMatrix) 
        {
            Matrix4x4 _ = Matrix;

            border1 = bound1;
            border2 = bound2;
            inhMatrix = InheritableMatrix;
        }

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
            needsMatrixUpdate = true;
            Changed?.Invoke();
        }


        private Matrix4x4 matrix;
        public Matrix4x4 Matrix
        {
            get
            {
                UpdateIfNecessary();
                return matrix;
            }
        }

        private void UpdateIfNecessary()
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
                    parent!.GetInheritedValues(out pborder1, out pborder2, out pmatrix);
                }
                else
                {
                    pborder2 = Game.Core.OpenGL.ScreenSize * 0.5f;
                    pborder1 = -pborder2;
                    pmatrix = Matrix4x4.Identity;
                }

                Matrix4x4 res = PivotMatrix;

                bound1 = pborder1 + (pborder2 - pborder1) * anchor1;
                bound1.X -= MarginLeft;
                bound1.Y -= MarginDown;

                bound2 = pborder1 + (pborder2 - pborder1) * anchor2;
                bound2.X += MarginRight;
                bound2.Y += MarginUp;

                res *= Matrix4x4.CreateScale(new Vector3(bound2 - bound1, 1));
                //res *= RotMatrix;
                res *= Matrix4x4.CreateTranslation(new Vector3((bound1 + bound2) * 0.5f, posZ));
                //res *= pmatrix;

                InheritableMatrix = Matrix4x4.Identity;

                matrix = res;
                needsMatrixUpdate = false;
            }
        }


        public UITransform(WorldObject linkedObject) : base(linkedObject, false)
		{
            Game.Core.OpenGL.Resized += () => needsMatrixUpdate = true;
            needsMatrixUpdate = true;
		}

		public override void Step()
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

        public Vector2 MarginCenter
        {
            get => new Vector2(MarginRight - MarginLeft, MarginUp - MarginDown) * 0.5f;
            set
            {
                Vector2 delta = value - new Vector2(MarginRight - MarginLeft, MarginUp - MarginDown) * 0.5f;

                marginRight += delta.X;
                marginLeft += delta.X;
                marginUp += delta.Y;
                marginDown += delta.Y;

                InvokeChanged();
            }
        }

        public Vector2 AnchorCenter
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
