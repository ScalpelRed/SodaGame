using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI.Bounds
{
    public abstract class UIBounds : UIModule
    {
        public event Action? Changed;

        protected bool NeedsUpdate = true;

        private Matrix4x4 transformInv;
        protected Matrix4x4 TransformInv { get => transformInv; }

        public UIBounds(WorldObject linkedObject) : base(linkedObject, false)
        {
            UITransform.Changed += InvokeChanged;
        }

        public abstract bool Contains(Vector2 pos);

        protected void UpdateIfNecessary()
        {
            if (NeedsUpdate)
            {
                Matrix4x4.Invert(UITransform.Matrix, out transformInv);
                Update();
            }
        }

        protected abstract void Update();

        protected void InvokeChanged()
        {
            NeedsUpdate = true;
            Changed?.Invoke();
        }
    }
}
