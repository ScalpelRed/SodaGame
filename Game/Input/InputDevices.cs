using System.Numerics;
using Silk.NET.Input;

using Triode.Game.Graphics;
using Triode;

namespace Triode.Game.Input
{
    public class InputDevices
    {
        private readonly List<InputDevice> inputDevices = new();

        internal void AddInputDevice(InputDevice device)
        {
            inputDevices.Add(device);
            DeviceAdded?.Invoke(device);
        }

        internal void RemoveInputDevice(InputDevice device)
        {
            inputDevices.Remove(device);
            DeviceRemoved?.Invoke(device);
        }

        public event Action<InputDevice>? DeviceAdded;
        public event Action<InputDevice>? DeviceRemoved;

        public T? GetFirstDevice<T>() where T : InputDevice
        {
            foreach (InputDevice v in inputDevices) if (v is T res) return res;
            return null;
        }

        public bool TryGetFirstDevice<T>(out T device) where T : InputDevice
        {
            foreach (InputDevice v in inputDevices)
            {
                if (v is T res)
                {
                    device = res;
                    return true;
                }
            }
            device = null!;
            return false;
        }

        public T[] GetAllDevices<T>() where T : InputDevice
        {
            List<T> res = new();
            foreach (InputDevice v in inputDevices) if (v is T dev) res.Add(dev);
            return res.ToArray();
        }

        public InputDevice[] GetAllDevices()
        {
            return inputDevices.ToArray();
        }
    }
}
