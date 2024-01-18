using Game.UI;
using System.Numerics;

namespace Game.Main
{
    public class WorldObject
    {
        public readonly GameController Game;

        private ITransform transform;
        public ITransform Transform
        {
            get => transform;
            set
            {
                transform = value;
                NewTransform?.Invoke(transform);
            }
        }
        public Action<ITransform>? NewTransform;

        private readonly List<ObjectModule> Modules;
        public Action<ObjectModule>? ModuleAttached;
        public Action<ObjectModule>? ModuleDetached;
        public Action<int>? ModuleOrderChanged;

        public WorldObject(Vector3 position, GameController game, ITransform? parent = null)
        {
            Game = game;
            Modules = [];
            transform = new Transform(position, parent);
        }

        public WorldObject(ITransform transform, GameController game)
        {
            Game = game;
            Modules = [];
            this.transform = transform;
        }

        internal void AddModule(ObjectModule module)
        {
            Modules.Add(module);
            ModuleAttached?.Invoke(module);
        }

        internal void RemoveModule(ObjectModule module)
        {
            Modules.Remove(module);
            ModuleDetached?.Invoke(module);
        }

        public T GetFirstModule<T>(bool allowInheritance = true) where T : ObjectModule
        {
            if (allowInheritance)
            {
                foreach (ObjectModule v in Modules) if (v is T t) return t;
            }
            else foreach (ObjectModule b in Modules) if (b.GetType() == typeof(T)) return (T)b;

            return null!;
        }

        public bool TryGetFirstModule<T>(out T module, bool allowInheritance = true) where T : ObjectModule
        {
            module = GetFirstModule<T>(allowInheritance);
            return module != null;
        }

        public T[] GetAllModules<T>(bool allowInheritance = true) where T : ObjectModule
        {
            List<T> res = new();

            if (allowInheritance)
            {
                foreach (ObjectModule v in Modules) if (v is T t) res.Add(t);
            }
            else foreach (ObjectModule b in Modules) if (b.GetType() == typeof(T)) res.Add((T)b);

            return res.ToArray();
        }

        public ObjectModule[] GetAllModules() => Modules.ToArray();

        public int GetModuleIndex(ObjectModule module) => Modules.IndexOf(module);

        public bool MoveModule(ObjectModule module, int newIndex)
        {
            int oldIndex = GetModuleIndex(module);
            if (oldIndex == -1) return false;

            Modules.RemoveAt(oldIndex);
            Modules.Insert(newIndex, module);

            ModuleOrderChanged?.Invoke(int.Min(newIndex, oldIndex));
            return true;
        }

        public void MoveModule(int oldIndex, int newIndex)
        {
            ObjectModule module = Modules[oldIndex];
            Modules.RemoveAt(oldIndex);
            Modules.Insert(newIndex, module);
            ModuleOrderChanged?.Invoke(int.Min(oldIndex, newIndex));
        }
    }
}