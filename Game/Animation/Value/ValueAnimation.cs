using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Animation.Value
{
    public class ValueAnimation<T>
    {

        public ITimedValueProvider<T> ValueProvider;

        private DateTime StartTime;
        private TimeSpan TimeOffset;

        public double Position
        {
            get
            {
                if (paused) return (PauseTime - StartTime + TimeOffset).TotalSeconds;
                return (DateTime.Now - StartTime + TimeOffset).TotalSeconds;
            } 
            set
            {
                double delta = value - Position;
                TimeOffset += new TimeSpan((long)(delta * 1E+07));
            }
        }

        private DateTime PauseTime;
        private bool paused;
        public bool Paused
        {
            get => paused;
            set
            {
                if (value && !paused) PauseTime = DateTime.Now;
                else if (paused && !value) TimeOffset -= DateTime.Now - PauseTime;

                paused = value;
            }
        }

        public ValueAnimation(ITimedValueProvider<T> valueProvider)
        {
            ValueProvider = valueProvider;

            TimeOffset = TimeSpan.Zero;
            PauseTime = DateTime.Now;
            StartTime = DateTime.Now;
            paused = false;

        }

        public void Start()
        {
            TimeOffset = TimeSpan.Zero;
            Paused = false;
            StartTime = DateTime.Now;
        }
    
        public T GetValue()
        {
            return ValueProvider.GetValue(Position);
        }

        public T GetValue(double seconds)
        {
            return ValueProvider.GetValue(seconds);
        }
    }
}
