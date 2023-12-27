using Game.Graphics;
using Game.Main;

namespace Game.UI
{
    public abstract class UIRenderer : Renderer, IUIModule
    {
        public UITransform UITransform { get; private set; }

        protected UIRenderer(WorldObject linkedObject) : base(linkedObject)
        {
            UITransform = linkedObject.TryGetModule(out UITransform uiTransform) ? uiTransform : new UITransform(linkedObject);
        }
    }
}
