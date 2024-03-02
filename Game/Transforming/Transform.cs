using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Transforming
{
    public interface Transform
    {
        public Transform? Parent { get; set; }
        public bool HasParent { get; }


        public event Action? Changed;
        public void UpdateIfNecessary();
        public void SeveralChanges(Action? changes);

        public Matrix4x4 FinalMatrix { get; }
        public Matrix4x4 InheritableMatrix { get; }

        public static T ValidateTransform<T>(Transform transform) where T : Transform
        {
            if (transform is not T tr) throw new IncompatibleTransformException(transform);
            return tr;
        }

        public static void ValidateTransform<T>(Transform transform, out T result) where T : Transform
        {
            if (transform is not T tr) throw new IncompatibleTransformException(transform);
            result = tr;
        }

    }
}
