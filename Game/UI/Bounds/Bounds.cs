using Game.Main;
using System.Numerics;

namespace Game.UI.Bounds
{
    public abstract class Bounds : ObjectModule
    {
        private Matrix4x4 transformInv;
        protected Matrix4x4 TransformInv => transformInv;

        protected Bounds(WorldObject linkedObject) : base(linkedObject)
        {
            SetChanged();
        }

        protected override void LinkWithTransform()
        {
            Transform.Changed += SetChanged;
            SetChanged();
        }

        protected override void UnlinkFromTransform()
        {
            Transform.Changed -= SetChanged;
        }

        public event Action? Changed;
        private bool needsUpdate = false;

        protected void SetChanged()
        {
            needsUpdate = true;
            Changed?.Invoke();
        }
        
        private void UpdateIfNecessary()
        {
            if (needsUpdate)
            {
                Matrix4x4.Invert(Transform.FinalMatrix, out transformInv);
                UpdateInh();
                needsUpdate = false;
            }
        }

        protected abstract void UpdateInh();

        protected abstract bool ContainsInh(Vector2 pos);

        public bool Contains(Vector2 pos)
        {
            UpdateIfNecessary();
            return ContainsInh(pos);
        }

        /*public event Action? Changed;

        protected bool NeedsUpdate = true;
        protected Matrix4x4 TransformInv;

        public Bounds(WorldObject linkedObject) : base(linkedObject)
        {

        }

        public abstract bool Contains(Vector2 pos);

        protected abstract void UpdateIfNecessary();

        protected void InvokeChanged()
        {
            NeedsUpdate = true;
            Changed?.Invoke();
        }*/
    }
}
