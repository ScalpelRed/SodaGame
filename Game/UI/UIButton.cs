using Game.Main;
using Silk.NET.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.UI
{
    public class UIButton : ObjectModule
    {
        protected UIBounds Bounds;

        public event Action<Vector2>? MouseIn;
        public event Action<Vector2>? MouseOut;
        public event Action<MouseButton>? MouseDown;
        public event Action<MouseButton>? MouseUp;

        private volatile bool hover = false;

        public UIButton(WorldObject linkedObject) : base(linkedObject, false)
        {
            if (!LinkedObject.TryGetModule(out UIBounds bounds)) throw new ModuleRequiredException<UIButton, UIBounds>();
            Bounds = bounds;
            setup();
        }

        public UIButton(UIBounds bounds) : base(bounds.LinkedObject, false)
        {
            Bounds = bounds;
            setup();
        }

        private void setup()
        {
            Game.Core.Input.MouseMove += (Vector2 pos) =>
            {
                if (Bounds.Contains(Game.MainCamera.WorldToScreen(pos)))
                {
                    if (!hover)
                    {
                        hover = true;
                        MouseIn?.Invoke(pos);
                    }
                }
                else
                {
                    if (hover)
                    {
                        hover = false;
                        MouseOut?.Invoke(pos);
                    }
                }
            };

            Game.Core.Input.MouseDown += (MouseButton button) =>
            {
                if (hover) MouseDown?.Invoke(button);
            };

            Game.Core.Input.MouseUp += (MouseButton button) =>
            {
                if (hover) MouseUp?.Invoke(button);
            };
        }

        public override void Step()
        {
            
        }
    }
}
