using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Triode.Game.Transforming;

namespace Triode.Game.General
{
    public class IncompatibleTransformException : Exception
    {
        public readonly Transform? Transform; 

        public IncompatibleTransformException(Transform? transform) : base($"Transform of this type is not compatible with this module.")
        {
            Transform = transform;
        }
    }
}
