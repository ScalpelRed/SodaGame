using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI
{
    public class TransformBounds : UIBounds
    {
        public TransformBounds(WorldObject linkedObject) : base(linkedObject)
        {
            Rectangle = Rectangle.Transform(Transform.GlobalMatrix);
            Transform.TransformChanged += () =>
            {
                Rectangle = Util.Rectangle.One.Transform(Transform.GlobalMatrix);
                InvokeChanged();
            };
        }
    }
}
