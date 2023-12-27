using Game.Graphics;
using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
