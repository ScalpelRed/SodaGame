namespace Game.Main
{
    public abstract class ObjectModule
    {
        public readonly WorldObject LinkedObject;

        public GameController Game
        {
            get => LinkedObject.Game;
        }

        private bool transformAccessible;
        private Transform? transform;
        public Transform Transform
        {
            get
            {
                if (transformAccessible) return transform!;
                else throw new DifferentTransformException();
            }
        }

        public ObjectModule(WorldObject worldObject)
        {
            LinkedObject = worldObject;
            worldObject.AddModule(this);
            OnNewTransform(worldObject.Transform);
            worldObject.NewTransform += OnNewTransform;
        }

        private void OnNewTransform(ITransform transform)
        {
            if (transform is Transform t)
            {
                transformAccessible = true;
                this.transform = t;
            }
            else
            {
                transformAccessible = false;
            }
        }

        public virtual void Step()
        {

        }
    }
}
