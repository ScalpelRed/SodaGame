using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Main
{
    public class DifferentTransformException : Exception
    {
        public DifferentTransformException() : base("Tried to get transform of another type " +
            "(did you tried to access default transform on worldobject with UIModules?)") 
        { 

        }
    }
}
