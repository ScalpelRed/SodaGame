using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Game.Graphics.OpenGL;

namespace Game.Graphics.Renderers
{
    public abstract class Renderer : ObjectModule
    {



        protected Renderer(WorldObject linkedObject) : base(linkedObject, true)
        {
            
        }

        protected Dictionary<string, object> Values = new();

        public void SetValue(string name, object value)
        {
            Values[name] = value;
        }

        public void AssignValuesDictionary(Dictionary<string, object> values)
        {
            Values = values;
        }

        public void UnassignValuesDictionary()
        {
            Values = new();
        }

        public abstract void Draw();

        public override void Step()
        {
            Draw();
        }
    }
}
