using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI.Bounds
{
    public class UITransformBounds : Bounds
    {
        public UITransformBounds(WorldObject linkedObject) : base(linkedObject)
        {
            UITransform.Changed += InvokeChanged;
        }

        public override bool Contains(Vector2 pos)
        {
            UpdateIfNecessary();
            pos = Vector2.Transform(pos, TransformInv);
            return pos.X >= -0.5f && pos.X <= 0.5f && pos.Y >= -0.5f && pos.Y <= 0.5f;
        }

        protected override void UpdateIfNecessary()
        {
            if (NeedsUpdate)
            {
                Matrix4x4.Invert(UITransform.Matrix, out TransformInv);
                NeedsUpdate = false;
            }
        }

        public override void Step()
        {

        }
    }
}
