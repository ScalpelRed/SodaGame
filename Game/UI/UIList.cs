﻿using Triode.Game.General;
using Triode.Game.Util;
using System.Numerics;

namespace Triode.Game.UI
{

    public class UIList : ObjectModule // TODO more directions
    {


        public UIList(WorldObject linkedObject) : base(linkedObject)
        {

        }

        protected override void Initialize()
        {
            
        }

        protected override void LinkWithObject()
        {
            
        }

        protected override void LinkWithTransform()
        {
            
        }

        protected override void UnlinkFromObject()
        {
            
        }

        protected override void UnlinkFromTransform()
        {
            
        }
    }

    /*public class UIList : ObjectModule
    {
        public readonly ListenableList<UITransform> Affected = [];

        private float offset;
        public float Offset
        {
            get => offset;
            set
            {
                offset = value;
                UpdatePositions();
            }
        }

        private volatile bool PauseUpdateEvent = false;

        public UIList(WorldObject linkedObject) : base(linkedObject)
        {
            Affected.Added += (UITransform uit, int _) =>
            {
                uit.Parent = Transform;
                uit.Changed += () => OnAffectedChange(uit);
                UpdatePositions();
            };
            Affected.Removed += (UITransform uit, int _) =>
            {
                uit.Parent = null;
                uit.Changed -= () => OnAffectedChange(uit);
                UpdatePositions();
            };
            Affected.OrderChanged += UpdatePositions;
        }

        public override void Step()
        {

        }

        private void OnAffectedChange(UITransform uit)
        {
            if (PauseUpdateEvent) return;
            if (uit.Parent != Transform) Affected.Remove(uit);
            else UpdatePositions();
        }

        public void UpdatePositions()
        {
            PauseUpdateEvent = true;

            Vector2 pos = new(Transform.Bound1.X, Transform.Bound2.Y);

            foreach (UITransform v in Affected)
            {
                Vector2 cpos = Transform.ToNormalized(pos + new Vector2(v.MarginLeft, -v.MarginUp));
                v.SeveralChanges(() =>
                {
                    v.AnchorRectLeft = cpos.X;
                    v.AnchorRectUp = cpos.Y;
                });
                pos.Y -= v.Bound2.Y - v.Bound1.Y + offset;
            }

            PauseUpdateEvent = false;
        }
    }*/
}
