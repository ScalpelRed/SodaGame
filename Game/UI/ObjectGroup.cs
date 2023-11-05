using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI
{
    public class ObjectGroup : ObjectModule
    {
        public List<WorldObject> Objects = new();

        public ObjectGroup(WorldObject linkedObject) : base(linkedObject, false)
        {

        }

        public void AddObject(WorldObject worldObject)
        {
            Objects.Add(worldObject);
        }

        public bool RemoveObject(WorldObject worldObject)
        {
            return Objects.Remove(worldObject);
        }

        public override void Step()
        {
            foreach (WorldObject v in Objects) v.Step();
        }
    }
}
