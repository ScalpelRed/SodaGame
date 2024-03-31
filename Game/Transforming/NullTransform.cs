using Triode.Game.Transforming;
using System.Numerics;

namespace Triode.Game.Transforming
{
    public class NullTransform : Transform
    {
        public static readonly NullTransform Null = new();

        private Transform? parent;
        public Transform? Parent
        {
            get => parent;
            set
            {
                parent = value;
                HasParent = value is null;
            }
        }

        public bool HasParent { get; private set; }

        public Matrix4x4 FinalMatrix => HasParent ? Parent!.InheritableMatrix : Matrix4x4.Identity;

        public Matrix4x4 InheritableMatrix => FinalMatrix;

        public event Action? Changed;

        public void SeveralChanges(Action? changes)
        {

        }

        public void UpdateIfNecessary()
        {

        }
    }
}
