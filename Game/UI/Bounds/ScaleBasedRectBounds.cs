using Triode.Game.General;
using System.Numerics;

namespace Triode.Game.UI.Bounds
{
    public class ScaleBasedRectBounds : Bounds
    {
        public ScaleBasedRectBounds(WorldObject linkedObject) : base(linkedObject)
        {

        }

        protected override bool ContainsInh(Vector2 pos)
        {
            pos = Vector2.Transform(pos, TransformInv);
            return pos.X >= -0.5f && pos.X <= 0.5f && pos.Y >= -0.5f && pos.Y <= 0.5f;
        }

        protected override void Initialize()
        {
            
        }

        protected override void LinkWithObject()
        {
            
        }

        protected override void UnlinkFromObject()
        {
            
        }

        protected override void UnlinkFromTransform()
        {
            
        }

        protected override void UpdateInh()
        {
            
        }


        /*public RectTransformBounds(WorldObject linkedObject) : base(linkedObject)
        {
            LinkedObject.TransformChanged += InvokeChanged;
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
                Matrix4x4.Invert(LinkedObject.Transform.FinalMatrix, out TransformInv);
                NeedsUpdate = false;
            }
        }

        public override void Step()
        {
            
        }*/
    }
}
