using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Main
{
    public interface ICloneableModule<T> where T : ObjectModule
    {
        public T Clone(WorldObject newLinkedObject);
    }
}
