using Game.Graphics;
using Game.Main;

namespace Game.UI
{
    public abstract class UIRenderer : Renderer // It should be Renderer xor UIModule. Renderer seems more important.
    {
        private bool transformIsCorrect;
        private UITransform? transform;
        public new UITransform Transform
        {
            get
            {
                if (transformIsCorrect) return transform!;
                else throw new DifferentTransformException();
            }
        }
        protected UIRenderer(WorldObject worldObject) : base(worldObject)
        {
            OnNewTransform(worldObject.Transform);
            worldObject.NewTransform += OnNewTransform;
        }

        private void OnNewTransform(ITransform transform)
        {
            if (transform is UITransform t)
            {
                transformIsCorrect = true;
                this.transform = t;
            }
            else
            {
                transformIsCorrect = false;
                this.transform = null;
            }
        }
    }
}
