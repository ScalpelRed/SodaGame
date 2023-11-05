using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI
{
    public class StretchPos : ObjectModule
    {
        public Transform Target1;
        public Transform Target2;
        public float Lerp;
        public bool Active = true;

        public StretchPos(WorldObject linkedObject, WorldObject target1, WorldObject target2, float lerp) : base(linkedObject, true)
        {
            Target1 = target1.Transform;
            Target2 = target2.Transform;
            Lerp = lerp;
        }

        public StretchPos(WorldObject linkedObject, Transform target1, Transform target2, float lerp) : base(linkedObject, true)
        {
            Target1 = target1;
            Target2 = target2;
            Lerp = lerp;
        }

        public override void Step()
        {
            if (Active) Transform.LocalPosition = Vector3.Lerp(Target1.LocalPosition, Target2.LocalPosition, Lerp);
        }
    }
}
