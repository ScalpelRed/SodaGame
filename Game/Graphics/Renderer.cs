using Game.Main;

namespace Game.Graphics
{
    public abstract class Renderer : ObjectModule
    {
        protected Renderer(WorldObject linkedObject) : base(linkedObject)
        {

        }

        protected Dictionary<string, object> Values = [];

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
            Values = [];
        }

        public abstract void Draw(Camera camera);

        public override void Step()
        {
            Draw(GameCore.Controller.MainCamera);
        }
    }
}
