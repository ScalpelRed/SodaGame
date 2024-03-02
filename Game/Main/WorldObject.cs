using Game.Transforming;
using Game.UI;
using Game.Util;
using System.Numerics;

namespace Game.Main
{
    public class WorldObject
    {
        public readonly GameCore GameCore;

        private Transform transform;
        public Transform Transform
        {
            get => transform;
            set
            {
                transform = value;
                NewTransform?.Invoke(value);
            }
        }
        public Action<Transform>? NewTransform;

        public WorldObject(GameCore gameCore)
        {
            transform = NullTransform.Null;
            GameCore = gameCore;
            Modules = [];
        }

        public WorldObject(Vector3 position, GameCore gameCore, Transform? parent = null)
        {
            GameCore = gameCore;
            Modules = [];
            transform = NullTransform.Null;
        }

        public WorldObject(Transform transform, GameCore gameCore)
        {
            GameCore = gameCore;
            Modules = [];
            this.transform = transform;
        }


        private readonly ListenableList<ObjectModule> Modules;
        public Action<ObjectModule>? ModuleAttached;
        public Action<ObjectModule>? ModuleDetached;
        public Action<int>? ModuleOrderChanged;

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