using Game.Animation.Interpolations;
using Game.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Animation
{
    public class FrameScale<T> : ITimeScale<T>
    {
        public readonly SortedDictionary<int, KeyFrame<T>> Keyframes = new();

        public int Fps = 60;

        public double Length
        {
            get => Keyframes.Keys.ElementAt(Keyframes.Count - 1) / Fps;
        }

        public T GetValue(float seconds)
        {
            int pos = (int)(seconds * Fps);

            if (Keyframes.TryGetByCloserLess(pos, out KeyFrame<T> frameLeft))
            {
                if (Keyframes.TryGetByCloserLess(pos, out KeyFrame<T> frameRight))
                    return frameLeft.OutInterpolation.Interpolate(frameLeft.Value, frameRight.Value, seconds - pos / Fps);

                return frameLeft.Value; // no frames next, return last value
            }
            else
            {
                if (Keyframes.TryGetByCloserLess(pos, out KeyFrame<T> frameRight))
                    return frameRight.Value; // no frames before, return first value

                throw new Exception("Animation has no frames.");
            }
        }
    }
}
