using Game.Util;
using System.Numerics;

namespace Game.Main
{
    public sealed class Transform
    {

        private Vector3 localPosition;
        private Matrix4x4 localPosMatrix;

        public Vector3 LocalPosition
        {
            get => localPosition;
            set
            {
                localPosition = value;
                localPosMatrix = Matrix4x4.CreateTranslation(localPosition);
                RecalculateMatrices();
            }
        }
        public Vector2 LocalPosition2
        {
            get => localPosition.XY();
            set
            {
                localPosition.X = value.X;
                localPosition.Y = value.Y;
                localPosMatrix = Matrix4x4.CreateTranslation(localPosition);
                RecalculateMatrices();
            }
        }

        private Vector3 globalPosition = Vector3.Zero;

        public Vector3 GlobalPosition
        {
            get => globalPosition;
            set
            {

                Vector3 delta = value - globalPosition;
                LocalPosition += delta;
                // no need to call RecalculateMatrices(), LocalPosition setter will do it
            }
        }
        public Vector2 GlobalPosition2
        {
            get => GlobalPosition.XY();
            set
            {
                Vector2 delta = value - globalPosition.XY();
                LocalPosition2 += delta;
                // same about RecalculateMatrices()
            }
        }
        private void RecalculateGlobalPos()
        {
            globalPosition = hasParent ? (localPosition + Parent!.GlobalPosition) : localPosition;
        }



        private Vector3 localRotation;
        private Matrix4x4 localRotMatrix;

        public Vector3 LocalRotation
        {
            get => localRotation;
            set
            {
                localRotation = value;
                localRotMatrix = Matrix4x4.CreateFromYawPitchRoll(value.X, value.Y, value.Z);
                RecalculateMatrices();
            }
        }
        public Vector2 LocalRotation2
        {
            get => localRotation.XY();
            set
            {
                localRotation.X = value.X;
                localRotation.Y = value.Y;
                localRotMatrix = Matrix4x4.CreateFromYawPitchRoll(value.X, value.Y, localRotation.Z);
                RecalculateMatrices();
            }
        }



        private Vector3 localScale;
        private Matrix4x4 localScaleMatrix;

        public Vector3 LocalScale
        {
            get => localScale;
            set
            {
                localScale = value;
                localScaleMatrix = Matrix4x4.CreateScale(value);
                RecalculateMatrices();
            }
        }
        public Vector2 LocalScale2
        {
            get => localScale.XY();
            set
            {
                localScale.X = value.X;
                localScale.Y = value.Y;
                localScaleMatrix = Matrix4x4.CreateScale(localScale);
                RecalculateMatrices();
            }
        }



        private Vector3 pivot;
        private Matrix4x4 pivotMatrix;

        public Vector3 Pivot
        {
            get => pivot;
            set
            {
                pivot = value;
                pivotMatrix = Matrix4x4.CreateTranslation(-value);
                RecalculateMatrices();
            }
        }
        public Vector2 Pivot2
        {
            get => pivot.XY();
            set
            {
                pivot.X = value.X;
                pivot.Y = value.Y;
                pivotMatrix = Matrix4x4.CreateTranslation(-pivot);
                RecalculateMatrices();
            }
        }



        private Transform? parent;
        private bool hasParent = false;

        public Transform? Parent
        {
            get => parent;
            set
            {
                if (parent != value)
                {
                    if (hasParent)
                    {
                        parent!.TransformChanged -= RecalculateMatrices;
                    }
                    hasParent = value is not null;
                    parent = value;
                    if (hasParent)
                    {
                        parent!.TransformChanged += RecalculateMatrices;
                    }
                    RecalculateMatrices();
                }
            }
        }
        public bool HasParent() => hasParent;


        public Matrix4x4 LocalMatrix { get; private set; }
        public Matrix4x4 GlobalMatrix { get; private set; }


        public event Action? TransformChanged;
        private void RecalculateMatrices()
        {
            LocalMatrix = pivotMatrix * localScaleMatrix * localRotMatrix * localPosMatrix;
            GlobalMatrix = hasParent ? (LocalMatrix * Parent!.GlobalMatrix) : LocalMatrix;
            RecalculateGlobalPos();
            TransformChanged?.Invoke();
        }

        public Transform(Vector3 pos, Transform? par = null)
        {
            LocalPosition = pos;
            LocalRotation = Vector3.Zero;
            LocalScale = Vector3.One;
            Pivot = Vector3.Zero;
            Parent = par;
        }
    }
}
