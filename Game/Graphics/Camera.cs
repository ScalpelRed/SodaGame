using System.Numerics;
using Game.Main;

namespace Game.Graphics
{
    public class Camera : ObjectModule
    {
        private float near;
        private float far;
        public readonly OpenGL Gl;

        public float Near 
        { 
            get => near;
            set
            {
                near = value;
                RecalculateViewMat();
            }
        }

        public float Far 
        { 
            get => far;
            set
            {
                far = value;
                RecalculateViewMat();
            }
        }

        public Matrix4x4 ViewMatrix { get; protected set; }

        protected void RecalculateViewMat()
        {
            ViewMatrix = Matrix4x4.CreateOrthographic(Gl.ScreenSize.X, Gl.ScreenSize.Y, Near, Far);
            needsMatrixUpdate = true;
        }


        private bool needsMatrixUpdate = false;
        private Matrix4x4 finalMatrix = Matrix4x4.Identity;
        private Matrix4x4 finalInverse = Matrix4x4.Identity;

        public Matrix4x4 FinalMatrix
        {
            get
            {
                if (needsMatrixUpdate)
                {
                    Matrix4x4.Invert(Transform.Matrix, out var TransformInv);
                    finalMatrix = TransformInv * ViewMatrix;

                    needsMatrixUpdate = false;
                }
                return finalMatrix;
            }
        }

        public Matrix4x4 FinalInverse
        {
            get
            {
                if (needsMatrixUpdate)
                {
                    Matrix4x4.Invert(FinalMatrix, out finalInverse);
                }
                return finalInverse;
            }
        }


        public Camera(WorldObject linkedObject, OpenGL gl, float far, float near = 0.05f) : base(linkedObject, false)
        {
            Gl = gl;
            this.far = far;
            this.near = near;
            RecalculateViewMat();

            Gl.Resized += RecalculateViewMat;
            Transform.Changed += () => needsMatrixUpdate = true;
        }


        public Vector2 ScreenToWorld(Vector2 screenPos)
        {
             return Vector2.Transform(screenPos, FinalInverse);
        }

        public Vector2 WorldToScreen(Vector2 worldPos)
        {
            return Vector2.Transform(worldPos, FinalMatrix) * Gl.ScreenSize * 0.5f;
        }

        public override void Step()
        {
            
        }
    }
}
