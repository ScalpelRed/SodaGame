using Game.Main;

namespace Game.UI
{
    public abstract class UIModule : ObjectModule, IUIModule
    {
        public UITransform UITransform { get; private set; }

        protected UIModule(WorldObject linkedObject, bool stepFromObject) : base(linkedObject, stepFromObject)
        {
            UITransform = linkedObject.TryGetModule(out UITransform uiTransform) ? uiTransform : new UITransform(linkedObject);
        }
    }
}
