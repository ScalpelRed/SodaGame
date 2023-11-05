using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI
{
    public abstract class UIBounds : ObjectModule
    {
        public Util.Rectangle Rectangle { get; protected set; }

        public event Action? Changed;

        public UIBounds(WorldObject linkedObject) : base(linkedObject, false)
        {
            Rectangle = Util.Rectangle.One;
        }

        protected void InvokeChanged()
        {
            Changed?.Invoke();
        }

        public bool Contains(Vector2 pos) => Rectangle.Contains(pos);

        public override void Step()
        {
            
        }
    }
}
