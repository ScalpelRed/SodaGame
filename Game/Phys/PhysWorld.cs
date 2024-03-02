using Game.Main;
using System.Numerics;
using Game.Util;

using Jitter;
using Jitter.LinearMath;
using Jitter.Collision;
using Jitter.Dynamics;

namespace Game.Phys
{
    /*public class PhysWorld
    {
        protected GameController Game;

        public Vector3 ConstantForce
        {
            get => constantForce;
            set 
            {
                constantForce = value;
                JitterWorld.Gravity = constantForce.ToJitter();
            }
        }
        private Vector3 constantForce;

        public readonly World JitterWorld;

        protected readonly SortedDictionary<RigidBody, PhysBody> PhysBodies = new();

        public readonly CollisionSystemSAP CollisionSystem;

        public PhysWorld(GameController game, Vector3 constantForce)
        {
            JitterWorld = new(CollisionSystem = new CollisionSystemSAP());
            ConstantForce = constantForce;

            CollisionSystem.CollisionDetected += (RigidBody body1, RigidBody body2, JVector point1, JVector point2, JVector normal, float penetration) =>
            {
                PhysBody pb1 = GetPhysBodyFor(body1);
                PhysBody pb2 = GetPhysBodyFor(body2);
                pb1.InvokeCollisionWith(pb2);
                pb2.InvokeCollisionWith(pb1);
            };

            Game = game;
        }

        public void Step()
        {
            JitterWorld.Step(Game.DeltaTime, false);
        }

        public void AddBody(PhysBody physBody)
        {
            PhysBodies.Add(physBody.JitterBody, physBody);
            JitterWorld.AddBody(physBody.JitterBody);
        }

        protected PhysBody GetPhysBodyFor(RigidBody rigidBody) => PhysBodies[rigidBody];
    }*/
}
