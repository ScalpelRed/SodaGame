using Game.Main;

namespace Game.UI
{
    public abstract class UIModule : ObjectModule
    {
        private bool transformAccessible;
        private UITransform? transform;
        public new UITransform Transform
        {
            get
            {
                if (transformAccessible) return transform!;
                else throw new DifferentTransformException();
            }
        }

        protected UIModule(WorldObject worldObject) : base(worldObject)
        {
            OnNewTransform(worldObject.Transform);
            worldObject.NewTransform += OnNewTransform;
        }

        private void OnNewTransform(ITransform transform)
        {
            if (transform is UITransform t)
            {
                transformAccessible = true;
                this.transform = t;
            }
            else
            {
                transformAccessible = false;
                this.transform = null;
            }
        }
    }
}
