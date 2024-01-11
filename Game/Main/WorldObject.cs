using Game.UI;
using System.Numerics;

namespace Game.Main
{
    public class WorldObject
    {
        public readonly Transform Transform;

        private readonly List<ObjectModule> Modules;

        public readonly GameController Game;

        public bool Disabled = false;

        public WorldObject(Vector3 position, GameController game, Transform? parent = null)
        {
            Game = game;
            Modules = [];
            Transform = new Transform(position, parent);
        }

        public WorldObject(GameController game, UITransform? parent = null) : this(Vector3.Zero, game, null)
        {
            new UITransformCont(this).UITransform.Parent = parent;
        }

        public void Step()
        {
            if (Disabled) return;
            foreach (ObjectModule module in Modules) module.Step();
        }

        internal bool AddModule(ObjectModule module)
        {
            if (!Modules.Contains(module))
            {
                Modules.Add(module);
                return true;
            }
            return false;
        }

        public T GetModule<T>() where T : ObjectModule
        {
            foreach (ObjectModule v in Modules) if (v is T t) return t;
            return null!;
        }

        public bool TryGetModule<T>(out T module) where T : ObjectModule
        {
            module = GetModule<T>();
            return module != null;
        }
    }
}