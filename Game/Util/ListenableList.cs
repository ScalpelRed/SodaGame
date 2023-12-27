namespace Game.Util
{
    public class ListenableList<T> : List<T>
    {
        public event Action<T, int>? Added;
        public event Action<T, int>? Removed;
        public event Action? OrderChanged;

        public new void Add(T item)
        {
            base.Add(item);
            Added?.Invoke(item, Count - 1);
        }

        public new void AddRange(IEnumerable<T> collection)
        {
            int index = Count;
            base.AddRange(collection);
            foreach (T item in collection)
            {
                Added?.Invoke(item, index);
                index++;
            }
        }

        public new void InsertRange(int index, IEnumerable<T> collection)
        {
            base.InsertRange(index, collection);
            foreach (T item in collection)
            {
                Added?.Invoke(item, index);
                index++;
            }
        }

        public new void Insert(int index, T item)
        {
            base.Insert(index, item);
            Added?.Invoke(item, index);
        }

        public new void Clear()
        {
            T[] items = ToArray();
            base.Clear();
            for (int i = 0; i < items.Length; i++) Removed?.Invoke(items[i], i);
        }

        public new bool Remove(T item)
        {
            int index = IndexOf(item);
            if (index == -1) return false;

            base.RemoveAt(index);
            Removed?.Invoke(item, index);
            return true;
        }
        
        public new int RemoveAll(Predicate<T> match)
        {
            throw new NotImplementedException();
        }

        public new void RemoveAt(int index)
        {
            T item = this[index];
            base.RemoveAt(index);
            Removed?.Invoke(item, index);
        }

        public new void RemoveRange(int index, int count) 
        {
            List<T> remitems = GetRange(index, count);
            base.RemoveRange(index, count);
            foreach (T item in remitems)
            {
                Removed?.Invoke(item, index);
                index++;
            }
        }

        public new void Reverse()
        {
            base.Reverse();
            OrderChanged?.Invoke();
        }

        public new void Reverse(int index, int count)
        {
            base.Reverse(index, count);
            OrderChanged?.Invoke();
        }

        
        public new void Sort(Comparison<T> comparison)
        {
            base.Sort(comparison);
            OrderChanged?.Invoke();
        }

        public new void Sort(int index, int count, IComparer<T>? comparer)
        {
            base.Sort(index, count, comparer);
            OrderChanged?.Invoke();
        }

        public new void Sort()
        {
            base.Sort();
            OrderChanged?.Invoke();
        }

        public new void Sort(IComparer<T>? comparer)
        {
            base.Sort(comparer);
            OrderChanged?.Invoke();
        }
    }
}
