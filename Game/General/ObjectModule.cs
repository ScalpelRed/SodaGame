using System.Diagnostics.CodeAnalysis;
using Triode.Game.Transforming;

namespace Triode.Game.General
{
    public abstract class ObjectModule
    {
        public readonly GameCore GameCore;
        public WorldObject LinkedObject { get; private set; }

        public Transform Transform
        {
            get => LinkedObject.Transform;
            set => LinkedObject.Transform = value;
        }

        public ObjectModule(WorldObject linkedObject)
        {
            GameCore = linkedObject.GameCore;
            LinkedObject = null!;
            InitAttachTo(linkedObject);
        }

        /// <summary>
        /// Called on first attacment when LinkedObject was set but before calling LinkWithObject
        /// </summary>
        protected abstract void Initialize();
        protected abstract void LinkWithObject();
        protected abstract void LinkWithTransform();
        protected abstract void UnlinkFromObject();
        protected abstract void UnlinkFromTransform();

        public event Action<WorldObject>? Reattached;

        public void AttachTo(WorldObject worldObject)
        {
            UnlinkFromTransform();
            UnlinkFromObject();
            LinkedObject.NewTransform -= ChangeTransform;
            LinkedObject.RemoveModule(this);

            LinkedObject = worldObject;
            LinkedObject.AddModule(this);
            LinkedObject.NewTransform += ChangeTransform;

            LinkWithObject();
            LinkWithTransform();

            Reattached?.Invoke(worldObject);
        }

        private void InitAttachTo(WorldObject worldObject)
        {
            LinkedObject = worldObject;
            LinkedObject.AddModule(this);
            LinkedObject.NewTransform += ChangeTransform;

            Initialize();
            LinkWithObject();
            LinkWithTransform();

            Reattached?.Invoke(worldObject);
        }

        private void ChangeTransform(Transform _)
        {
            UnlinkFromTransform();
            LinkWithTransform();
        }

        public virtual void Step()
        {

        }
    }
}
