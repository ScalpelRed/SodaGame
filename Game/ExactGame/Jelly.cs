﻿using Triode.Game.Graphics;
using Triode.Game.General;
using Triode.Game.OtherAssets;
using System.Numerics;

namespace Triode.Game.ExactGame
{
    /*public class Jelly : ObjectModule
    {
        readonly Renderer rend;

        public float Stretching = 1.5f;

        public Vector3 Bone;

        public Jelly(WorldObject linkedObject) : base(linkedObject)
        {
            if (!linkedObject.TryGetFirstModule(out rend)) throw new ModuleRequiredException<Jelly, Renderer>();
        }

        public override void Step()
        {
            rend.SetValue("bone", Bone);
        }

        public static GlMesh CreateRiggedGlMesh(OpenGL gl, RawMesh mesh, Vector3 lockPoint)
        {
            Vector3[] verts = mesh.GetPosArray();

            float[] dists = new float[verts.Length];
            float maxDist = 0;
            int i = 0;
            foreach (Vector3 v in verts)
            {
                float dist = (v - lockPoint).Length();
                if (dist > maxDist) maxDist = dist;
                dists[i] = dist;
                i++;
            }

            dists = dists.Select(x => x / maxDist).ToArray();

            return new GlMesh(gl, mesh, dists);
        }
    }*/
}
