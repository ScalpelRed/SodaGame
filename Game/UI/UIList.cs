using Game.Main;
using Game.Util;
using System.Numerics;

namespace Game.UI
{
    public class UIList : UIModule
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
                uit.Parent = UITransform;
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
            if (uit.Parent != UITransform) Affected.Remove(uit);
            else UpdatePositions();
        }

        public void UpdatePositions()
        {
            PauseUpdateEvent = true;

            Vector2 pos = new(UITransform.Bound1.X, UITransform.Bound2.Y);

            foreach (UITransform v in Affected)
            {
                Vector2 cpos = UITransform.ToNormalized(pos + new Vector2(v.MarginLeft, -v.MarginUp));
                v.AnchorRectLeft = cpos.X;
                v.AnchorRectUp = cpos.Y;
                pos.Y -= v.Bound2.Y - v.Bound1.Y + offset;
            }

            PauseUpdateEvent = false;
        }
    }
}
