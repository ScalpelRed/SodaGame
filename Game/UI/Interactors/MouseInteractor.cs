﻿using Game.Main;
using Game.Util;
using Silk.NET.Input;
using System.Numerics;

namespace Game.UI.Interactors
{
    public class MouseInteractor : ObjectModule
    {
        /*  MouseInteractor - provides events, has list of other interactors, events from them fires same events on this interactor
        *      Button - simplest one: no special events
        *          ModelButton - changes UIModelRenderer's model
        *          TexturedButton - changes texture
        *      Toggle - has event StateChanged(bool state)
        *          ModelToggle, TexturedToggle, etc
        *          Switch - changes child object position
        *       
        *  UIBounds - bounds to check clicks
        *      TransformBounds - bounding box is a rectangle of scale 1
        */

        public ListenableList<Bounds.Bounds> RelatedBounds = new();

        public event Action<Vector2>? MouseIn;
        public event Action<Vector2>? MouseOut;
        public event Action<MouseButton>? MouseDown;
        public event Action<MouseButton>? MouseUp;

        public MouseInteractor(WorldObject linkedObject) : base(linkedObject)
        {
            
        }

        protected override void Initialize()
        {
            RelatedBounds.Added += (bounds, _) =>
            {
                bounds.Changed
            };
        }

        protected override void LinkWithObject()
        {
            
        }

        protected override void LinkWithTransform()
        {
            
        }

        protected override void UnlinkFromObject()
        {
            RelatedBounds.Clear(); // Most likely all the bounds are on the old object and should not be used anymore
            // if not, they should be reattached and added again
        }

        protected override void UnlinkFromTransform()
        {
            
        }

        private bool Hover;

        private void CheckHover()
        {
            for (int i = 0; i < RelatedBounds.Count; i++)
            {
                if (RelatedBounds[i].Contains())
                {

                }
            }
        }

        /*protected Bounds.Bounds Bounds;

        /*public MouseInteractor(WorldObject linkedObject) : base(linkedObject)
        {
            if (!LinkedObject.TryGetFirstModule(out Bounds.Bounds bounds)) throw new ModuleRequiredException<MouseInteractor, Bounds.Bounds>();
            Bounds = bounds;
            setup();
        }

        public MouseInteractor(Bounds.Bounds bounds) : base(bounds.LinkedObject)
        {
            Bounds = bounds;
            setup();
        }

        private volatile bool Hover = false;

        private void setup()
        {
            void onMove(Vector2 pos)
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
            }

            Game.Core.Input.MouseMove += onMove;
            // TODO onMove also should be called when transform changes

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

        }*/
    }
}
