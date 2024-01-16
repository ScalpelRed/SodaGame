namespace Game.Main
{
    public abstract class ObjectModule
    {
        public readonly WorldObject LinkedObject;

        public GameController Game
        {
            get => LinkedObject.Game;
        }

        public Transform Transform
        {
            get => LinkedObject.Transform;
        }

        public ObjectModule(WorldObject worldObject)
        {
            LinkedObject = worldObject;
            worldObject.AddModule(this);
        }

        public abstract void Step();
    }
}
