using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using Game.Main;

namespace Game.Graphics
{
    public class Camera : ObjectModule
    {
        private float near;
        private float far;
        public readonly OpenGL Gl;

        public Vector3 LocalPosition
        {
            get => Transform.LocalPosition;
            set => Transform.LocalPosition = value;
        }

        public Vector3 GlobalPosition
        {
            get => Transform.GlobalPosition;
        }

        public float Near 
        { 
            get => near;
            set
            {
                near = value;
                RecalculateMatrices();
            }
        }

        public float Far 
        { 
            get => far;
            set
            {
                far = value;
                RecalculateMatrices();
            }
        }

        public Camera(WorldObject linkedObject, OpenGL gl, float far, float near = 0.05f) : base(linkedObject, false)
        {
            Gl = gl;
            Far = far;
            Near = near;
            RecalculateMatrices();

            Gl.Resized += () => RecalculateMatrices();
            Transform.Changed += () => RecalculateMatrices();
        }

        private Matrix4x4 viewMatrix;
        private Matrix4x4 transformInv;

        private void RecalculateMatrices()
        {
            Matrix4x4.Invert(Transform.GlobalMatrix, out transformInv);
            viewMatrix = transformInv * Matrix4x4.CreateOrthographic(Gl.ScreenSize.X, Gl.ScreenSize.Y, Near, Far);
        }

        public Matrix4x4 GetMatrix()
        {
            return viewMatrix;
        }

        public Vector2 ScreenToWorld(Vector2 screenPos)
        {
            return Vector2.Transform(screenPos, Transform.GlobalMatrix);
        }

        public Vector2 WorldToScreen(Vector2 worldPos)
        {
            return Vector2.Transform(worldPos, transformInv);
        }

        public override void Step()
        {
            
        }
    }
}
