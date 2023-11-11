﻿using Game.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets
{
    public sealed class Asset<K, V>
    {
        private Dictionary<K, V> List = new();

        public readonly GameCore Core;
        private readonly Func<K, V> CreateFunc;

        public Asset(GameCore core, Func<K, V> createFunc)
        {
            Core = core;
            CreateFunc = createFunc;
        }

        public V Get(K key) 
        {
            try
            {
                return List[key];
            }
            catch (KeyNotFoundException)
            {
                V res = CreateFunc(key);
                List.Add(key, res);
                return res;
            }
        }

        public bool TryGet(K key, out V res)
        {
            return List.TryGetValue(key, out res!);
        }

        public void Enlist(K key, V value)
        {
            List[key] = value;
        }
    }
}
