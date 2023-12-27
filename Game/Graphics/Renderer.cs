using Game.Main;

namespace Game.Graphics
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

        public abstract void Draw(Camera camera);

        public override void Step()
        {
            Draw(Game.MainCamera);
        }
    }
}
