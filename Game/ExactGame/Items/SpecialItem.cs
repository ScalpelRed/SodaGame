using Triode.Game.Graphics;
using Triode.Game.General;
using System.Numerics;

namespace Triode.Game.ExactGame.Items
{
    public abstract class SpecialItem : Item
    {
        public SpecialItem(string rawName, GameCore core) : base(rawName, core)
        {
            
        }

        public abstract WorldObject InstantiateSodaObject();
    }
}
