using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Main
{
    public interface ITransform
    {
        public ITransform? Parent { get; set; }
        public bool HasParent { get; }

        public event Action? Changed;
        public void UpdateIfNecessary();

        public Matrix4x4 Matrix { get; }
        public Matrix4x4 InheritableMatrix { get; }

    }
}
