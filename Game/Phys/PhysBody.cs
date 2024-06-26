﻿using Triode.Game.General;
using System.Numerics;
using Triode.Game.Util;
using Jitter.Dynamics;
using Jitter.Collision.Shapes;
using Jitter.LinearMath;

namespace Triode.Game.Phys
{
    /*public class PhysBody : ObjectModule
    {
        public readonly PhysWorld PhysWorld;

        public readonly RigidBody JitterBody;

        private bool transformUpdated;

        public PhysBody(WorldObject linkedObject, float mass, PhysWorld world, Shape collisionShape) : base(linkedObject)
        {
            JitterBody = new RigidBody(collisionShape)
            {
                Position = Transform.GlobalPosition.ToJitter(),
                Mass = mass,
                Damping = RigidBody.DampingType.None,
            };
            PhysWorld = world;
            world.AddBody(this);

            LinkedObject.TransformChanged += () => transformUpdated = true;
        }

        public override void Step()
        {
            if (transformUpdated)
            {
                JitterBody.Position = Transform.GlobalPosition.ToJitter();

                transformUpdated = false;
            }
            else
            {
                Transform.GlobalPosition = JitterBody.Position.ToNumerics();
            }
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
    }*/
}
