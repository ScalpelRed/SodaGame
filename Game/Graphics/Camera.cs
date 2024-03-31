using System.Numerics;
using Triode.Game.General;

namespace Triode.Game.Graphics
{
    public class Camera : ObjectModule
    {
        private float near;
        public float Near
        {
            get => near;
            set
            {
                near = value;
                SetViewMatChanged();
            }
        }
        
        private float far;
        public float Far
        {
            get => far;
            set
            {
                far = value;
                SetViewMatChanged();
            }
        }

        private float width;
        public float Width
        {
            get => width;
            set
            {
                width = value;
                SetViewMatChanged();
            }
        }

        private float height;
        public float Height
        {
            get => height;
            set
            {
                height = value;
                SetViewMatChanged();
            }
        }


        private bool needsViewmatUpdate = false;
        private Matrix4x4 viewMatrix;

        public Matrix4x4 ViewMatrix
        {
            get
            {
                if (needsViewmatUpdate)
                {
                    viewMatrix = Matrix4x4.CreateOrthographic(width, height, near, far);
                    needsViewmatUpdate = false;
                }
                return viewMatrix;
            }
        }

        protected void SetViewMatChanged()
        {
            needsViewmatUpdate = true;
            needsFinalmatUpdate = true;
        }


        private bool needsFinalmatUpdate = false;
        private Matrix4x4 finalMatrix;

        public Matrix4x4 FinalMatrix
        {
            get
            {
                if (needsFinalmatUpdate) // TODO transform updates even if only view changed, split into transform and view update
                {
                    Matrix4x4.Invert(Transform.FinalMatrix, out finalMatrix);
                    finalMatrix *= ViewMatrix;
                    needsFinalmatUpdate = false;
                }
                return finalMatrix;
            }
        }

        protected void SetFinalMatChanged() // to avoid large code on transform change attachment
        {
            needsFinalmatUpdate = true;
        }

        private Matrix4x4 finalInverse;
        public Matrix4x4 FinalInverse
        {
            get
            {
                if (needsFinalmatUpdate) Matrix4x4.Invert(FinalMatrix, out finalInverse);
                return finalInverse;
            }
        }

        public Camera(float width, float height, float far, float near, WorldObject linkedObject) : base(linkedObject)
        {
            this.width = width;
            this.height = height;
            this.far = far;
            this.near = near;
            SetViewMatChanged();
        }

        protected override void Initialize()
        {
            // none
        }

        protected override void LinkWithObject()
        {
            // none
        }

        protected override void LinkWithTransform()
        {
            LinkedObject.Transform.Changed += SetFinalMatChanged;
            SetFinalMatChanged();
        }

        protected override void UnlinkFromObject()
        {
            // none
        }

        protected override void UnlinkFromTransform()
        {
            LinkedObject.Transform.Changed -= SetFinalMatChanged;
        }

        public Vector2 ScreenToWorld(Vector2 screenPos)
        {
            return Vector2.Transform(screenPos, FinalInverse);
        }

        public Vector2 WorldToScreen(Vector2 worldPos)
        {
            return Vector2.Transform(worldPos, FinalMatrix) * new Vector2(width, height) * 0.5f;
        }
    }











    /*public class Camera : ObjectModule
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
                    Matrix4x4.Invert(Transform.FinalMatrix, out var TransformInv);
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


        public Camera(WorldObject linkedObject, OpenGL gl, float far, float near = 0.05f) : base(linkedObject)
        {
            Gl = gl;
            this.far = far;
            this.near = near;
            RecalculateViewMat();

            Gl.Resized += RecalculateViewMat;
            LinkedObject.TransformChanged += () => needsMatrixUpdate = true;
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
    }*/
}
