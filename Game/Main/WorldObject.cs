using Game.UI;
using System.Numerics;

namespace Game.Main
{
    public class WorldObject
    {
        public readonly GameController Game;
        public readonly Transform Transform;
        private readonly List<ObjectModule> Modules;

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

        public Action<ObjectModule>? ModuleAttached;
        public Action<ObjectModule>? ModuleDetached;
        public Action<int>? ModuleOrderChanged;

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

        public bool SetModuleIndex(ObjectModule module, int index)
        {
            int ci = GetModuleIndex(module);
            if (ci == -1) return false;

            Modules.RemoveAt(ci);

            if (index < 0 || index >= Modules.Count) throw new IndexOutOfRangeException();
            Modules.Insert(index, module);
            ModuleOrderChanged?.Invoke(int.Min(index, ci));
            return true;
        }
    }
}