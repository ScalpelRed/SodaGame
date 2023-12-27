﻿using Game.Main;
using Game.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.ExactGame.Items
{
    public abstract class Item
    {
        public readonly TranslatableString Name;

        protected double count;
        public double Count
        {
            get => count;
            set
            {
                count = value;
                CountChanged?.Invoke(value);
            }
        }
        public event Action<double>? CountChanged;

        public readonly GameCore Core;

        public Item(string rawName, GameCore core)
        {
            Core = core;
            Name = new(rawName);
        }

        public abstract WorldObject InstantiateDisplay();
    }
}
