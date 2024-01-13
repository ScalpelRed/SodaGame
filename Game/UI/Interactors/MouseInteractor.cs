using Game.Main;
using Game.UI.Bounds;
using Silk.NET.Input;
using System.Numerics;

namespace Game.UI.Interactors
{
    public class MouseInteractor : UIModule
    {
        protected Bounds.Bounds Bounds;

        public event Action<Vector2>? MouseIn;
        public event Action<Vector2>? MouseOut;
        public event Action<MouseButton>? MouseDown;
        public event Action<MouseButton>? MouseUp;

        /*  MouseInteractor - provides events, has list of other interactors, events from them fires same events on this interactor
         *      Button - simplest one: no special events
         *          ModelButton - changes UIModelRenderer's model
         *          TexturedButton - changes texture
         *      Toggle - has event StateChanged(bool state)
         *          ModelToggle, TexturedToggle, etc
         *          Switch - changes child object position
         *       
         *  UIBounds - bounds to check clicks
         *      TransformBounds - bounding box is rectangle transformed by transform
         *      
        */

        /*public static float DefaultIgnoreUpDelta = 50;
        public float IgnoreUpDelta = DefaultIgnoreUpDelta;
        protected volatile float DownX;
        protected volatile float DownY;*/

        public MouseInteractor(WorldObject linkedObject) : base(linkedObject, false)
        {
            if (!LinkedObject.TryGetModule(out Bounds.Bounds bounds)) throw new ModuleRequiredException<MouseInteractor, Bounds.Bounds>();
            Bounds = bounds;
            setup();
        }

        public MouseInteractor(Bounds.Bounds bounds) : base(bounds.LinkedObject, false)
        {
            Bounds = bounds;
            setup();
        }

        private volatile bool Hover = false;

        private void setup()
        {
            Game.Core.Input.MouseMove += (pos) =>
            {
                if (Bounds.Contains(Game.MainCamera.WorldToScreen(pos)))
                {
                    if (!Hover)
                    {
                        Hover = true;
                        MouseIn?.Invoke(pos);
                    }
                }
                else
                {
                    if (Hover)
                    {
                        Hover = false;
                        MouseOut?.Invoke(pos);
                    }
                }
            };

            Game.Core.Input.MouseDown += (button) =>
            {
                if (Hover) MouseDown?.Invoke(button);
            };

            Game.Core.Input.MouseUp += (button) =>
            {
                if (Hover) MouseUp?.Invoke(button);
            };
        }

        public override void Step()
        {

        }
    }
}
