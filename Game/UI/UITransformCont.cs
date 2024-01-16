using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI
{
    public class UITransformCont : ObjectModule
    {
        public readonly UITransform UITransform;

        public UITransformCont(WorldObject linkedObject) : base(linkedObject)
        {
            UITransform = new UITransform(Game.Core.OpenGL);
        }

        public override void Step()
        {
            
        }
    }
}
