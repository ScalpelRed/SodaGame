using Game.Util;
using System.Numerics;

namespace Game.Transforming
{
    public sealed class WorldTransform : Transform
    {
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



        private Vector3 scale = Vector3.One;
        private Matrix4x4 ScaleMatrix = Matrix4x4.Identity;

        public Vector3 Scale
        {
            get => scale;
            set
            {
                scale = value;
                ScaleMatrix = Matrix4x4.CreateScale(value);
                InvokeChanged();
            }
        }

        public Vector2 Scale2
        {
            get => scale.XY();
            set => Scale = new Vector3(value, scale.Z);
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



        private Vector3 localPosition = Vector3.Zero;
        private Matrix4x4 LocalPosMatrix = Matrix4x4.Identity;

        public Vector3 LocalPosition
        {
            get => localPosition;
            set
            {
                Vector3 delta = value - localPosition;
                globalPosition += delta;

                localPosition = value;
                LocalPosMatrix = Matrix4x4.CreateTranslation(localPosition);

                InvokeChanged();
            }
        }

        public Vector2 LocalPosition2
        {
            get => localPosition.XY();
            set => LocalPosition = new Vector3(value, localPosition.Z);
        }


        private Vector3 globalPosition = Vector3.Zero;

        public Vector3 GlobalPosition
        {
            get => globalPosition;
            set
            {
                Vector3 delta = value - globalPosition;
                localPosition += delta;

                globalPosition = value;
                LocalPosMatrix = Matrix4x4.CreateTranslation(localPosition);

                InvokeChanged();
            }
        }

        public Vector2 GlobalPosition2
        {
            get => GlobalPosition.XY();
            set => GlobalPosition = new Vector3(value, globalPosition.Z);
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

                InvokeChanged();
            }
        }

        public bool HasParent { get; private set; }



        private bool needsUpdate = false;
        public event Action? Changed;
        private void InvokeChanged()
        {
            if (severalChanges) return;
            needsUpdate = true;
            Changed?.Invoke();
        }


        private Matrix4x4 matrix = Matrix4x4.Identity;

        public Matrix4x4 FinalMatrix
        {
            // TODO optimize, it processes all the values even if only one changed
            // may be done with bit mask to avoid using boolean array

            get
            {
                UpdateIfNecessary();
                return matrix;
            }
        }

        public Matrix4x4 InheritableMatrix
        {
            get => FinalMatrix;
        }

        public void UpdateIfNecessary()
        {
            if (needsUpdate)
            {
                matrix = PivotMatrix * ScaleMatrix * RotMatrix * LocalPosMatrix * (HasParent ? parent!.InheritableMatrix : Matrix4x4.Identity);
                needsUpdate = false;
            }
        }

        private bool severalChanges = false;
        public void SeveralChanges(Action? changes)
        {
            severalChanges = true;
            changes?.Invoke();
            severalChanges = false;
            InvokeChanged();
        }

        public WorldTransform(Vector3 pos, Transform? parent = null)
        {
            LocalPosition = pos;
            Parent = parent;
            needsUpdate = true;
        }
    }
}