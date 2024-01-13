using Game.Main;
using System.Numerics;

namespace Game.UI.Bounds
{
    public abstract class Bounds : UIModule
    {
        public event Action? Changed;

        protected bool NeedsUpdate = true;
        protected Matrix4x4 TransformInv;

        public Bounds(WorldObject linkedObject) : base(linkedObject, false)
        {

        }

        public abstract bool Contains(Vector2 pos);

        protected abstract void UpdateIfNecessary();

        protected void InvokeChanged()
        {
            NeedsUpdate = true;
            Changed?.Invoke();
        }
    }
}
