﻿using Game.Main;
using System.Numerics;

namespace Game.UI.Bounds
{
    public class TransformBounds : Bounds
    {

        public TransformBounds(WorldObject linkedObject) : base(linkedObject)
        {
            Transform.Changed += InvokeChanged;
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
                Matrix4x4.Invert(Transform.Matrix, out TransformInv);
                NeedsUpdate = false;
            }
        }

        public override void Step()
        {
            
        }
    }
}
