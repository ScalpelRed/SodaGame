using Triode.Game.General;
using Triode.Game.Util;

namespace Triode.Game.ExactGame.Items
{
    public abstract class Item
    {
        public readonly TranslatableString Name;

        protected float count;
        public float Count
        {
            get => count;
            set
            {
                count = value;
                CountChanged?.Invoke(value);
            }
        }
        public event Action<float>? CountChanged;

        public readonly GameCore Core;

        public Item(string rawName, GameCore core)
        {
            Core = core;
            Name = new(rawName);
        }

        public abstract WorldObject InstantiateIcon();
    }
}
