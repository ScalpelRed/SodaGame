using Game.Main;
using Game.UI.Bounds;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
