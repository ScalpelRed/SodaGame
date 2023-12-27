using Game.Graphics;
using Game.Main;

namespace Game.UI
{
    public abstract class UIRenderer : Renderer, IUIModule
    {
        public UITransform UITransform { get; private set; }

        protected UIRenderer(WorldObject linkedObject) : base(linkedObject)
        {
            UITransform = (linkedObject.TryGetModule(out UITransformCont uiTransform) ? uiTransform : new UITransformCont(linkedObject)).UITransform;
        }
    }
}
