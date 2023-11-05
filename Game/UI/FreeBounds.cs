using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI
{
    public class FreeBounds : UIBounds
    {
        public FreeBounds(WorldObject linkedObject) : base(linkedObject)
        {
            
        }

        public void SetCorners(Vector2 corner1, Vector2 corner2)
        {
            Rectangle = new Util.Rectangle(corner1, corner2);
        }

        public void SetCenterAndSize(Vector2 center, Vector2 size)
        {
            Rectangle = Util.Rectangle.ByCenterAndSize(center, size);
        }
    }
}