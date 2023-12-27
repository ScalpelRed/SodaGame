using Game.Main;
using Game.UI.Bounds;

namespace Game.UI.Interactors
{
    public class UIButton : MouseInteractor
    {
        public UIButton(WorldObject linkedObject) : base(linkedObject)
        {

        }

        public UIButton(UIBounds bounds) : base(bounds)
        {

        }
    }
}
