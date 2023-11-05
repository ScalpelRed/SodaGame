using Game.Main;
using System.Numerics;
using Game.Util;
using Jitter.Dynamics;
using Jitter.Collision.Shapes;
using Jitter.Collision;
using Jitter.LinearMath;

namespace Game.Phys
{
    public class PhysBody : ObjectModule
    {
        public readonly PhysWorld PhysWorld;

        public readonly RigidBody JitterBody;

        private volatile bool allowTransformUpdating = true;

        public PhysBody(WorldObject linkedObject, float mass, PhysWorld world, Shape collisionShape) : base(linkedObject, true)
        {
            JitterBody = new RigidBody(collisionShape)
            {
                Position = Transform.GlobalPosition.ToJitter(),
                Mass = mass,
                Damping = RigidBody.DampingType.None,
            };
            PhysWorld = world;
            world.AddBody(this);

            Transform.TransformChanged += () =>
            {
                if (allowTransformUpdating) JitterBody.Position = Transform.GlobalPosition.ToJitter();
            };
        }

        public override void Step()
        {
            allowTransformUpdating = false;
            Transform.GlobalPosition = JitterBody.Position.ToNumerics();
            allowTransformUpdating = true;
        }

        public float Mass
        {
            get => JitterBody.Mass;
            set
            {
                JitterBody.Mass = value;
                MassChanged?.Invoke(value);
            }
        }
        public event Action<float>? MassChanged;

        public Vector3 Velocity
        {
            get => JitterBody.LinearVelocity.ToNumerics();
            set => JitterBody.LinearVelocity = value.ToJitter();
        }

        public Action<PhysBody>? Collision;
        public void InvokeCollisionWith(PhysBody body)
        {
            Collision?.Invoke(body);
        }

        public void AddForce(Vector3 force)
        {
            JitterBody.AddForce(force.ToJitter());
        }

        public void AddTorque(Vector3 torque)
        {
            JitterBody.AddTorque(torque.ToJitter());
        }

        public void AddTorqueZ(float torque)
        {
            JitterBody.AddTorque(JVector.Backward * torque);
        }

        public void AddVelocity(Vector3 velocity)
        {
            JitterBody.LinearVelocity += velocity.ToJitter();
        }

        public void AddAngularVelocity(Vector3 angularVelocity)
        {
            JitterBody.AngularVelocity += angularVelocity.ToJitter();
        }

        public void AddAngularVelocityZ(float angularVelocity)
        {
            JitterBody.AngularVelocity += JVector.Backward * angularVelocity;
        }
    }
}
