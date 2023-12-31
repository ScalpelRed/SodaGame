﻿using Game.Main;
using System.Numerics;

namespace Game.UI.Bounds
{
    public class TransformBounds : UIBounds
    {

        public TransformBounds(WorldObject linkedObject) : base(linkedObject)
        {

        }

        public override bool Contains(Vector2 pos)
        {
            UpdateIfNecessary();
            pos = Vector2.Transform(pos, TransformInv);
            return pos.X >= -0.5f && pos.X <= 0.5f && pos.Y >= -0.5f && pos.Y <= 0.5f;
        }

        protected override void Update()
        {
            
        }

        public override void Step()
        {
            
        }
    }
}
