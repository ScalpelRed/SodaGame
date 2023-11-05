using Game.Audio;
using Game.Graphics;
using Game.OtherAssets;
using Game.Phys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game.Main
{
    public class Assets
    {
        public readonly GameCore Core;

        public Assets(GameCore core)
        {
            Core = core;

            Textures.Add("", new GlTexture());
            Shaders.Add("", new GlShader(core.OpenGL));
        }

        private readonly SortedDictionary<string, GlShader> Shaders = new();
        public GlShader GetShader(string name)
        {
            try
            {
                return Shaders[name];
            }
            catch (KeyNotFoundException)
            {
                GlShader shader = new(name, Core.OpenGL);
                Shaders.Add(name, shader);
                return shader;
            }
        }

        private readonly SortedDictionary<string, RawTexture> RawTextures = new();
        public RawTexture GetRawTexture(string name)
        {
            try
            {
                return RawTextures[name];
            }
            catch (KeyNotFoundException)
            {
                RawTexture rawtexture = new(name, Core);
                RawTextures.Add(name, rawtexture);
                return rawtexture;
            }

        }

        private readonly SortedDictionary<string, GlTexture> Textures = new();
        public GlTexture GetTexture(string name)
        {
            try
            {
                return Textures[name];
            }
            catch (KeyNotFoundException)
            {
                GlTexture texture = new(name, Core.OpenGL);
                Textures.Add(name, texture);
                return texture;
            }

        }

        private readonly SortedDictionary<string, RawMesh> RawMeshes = new();
        public RawMesh GetRawMesh(string name)
        {
            try
            {
                return RawMeshes[name];
            }
            catch (KeyNotFoundException)
            {
                string[] source;
                try
                {
                    source = Core.Platform.IO.ReadAllLinesReadable("meshes/" + name + ".obj");
                }
                catch (FileNotFoundException)
                {
                    throw new AssetNotFoundException("Mesh", name);
                }
                RawMesh mesh = new(source);
                RawMeshes.Add(name, mesh);
                return mesh;
            }

        }

        private readonly SortedDictionary<string, GlMesh> GlMeshes = new();
        public GlMesh GetGlMesh(string name)
        {
            try
            {
                return GlMeshes[name];
            }
            catch (KeyNotFoundException)
            {
                GlMesh glmesh = new(Core.OpenGL, GetRawMesh(name));
                GlMeshes.Add(name, glmesh);
                return glmesh;
            }
        }

        /*private readonly SortedDictionary<string, PhysicalMesh2D> PhysMeshes = new();
        public PhysicalMesh2D GetPhysicalMesh(string name)
        {
            try
            {
                return PhysMeshes[name];
            }
            catch (KeyNotFoundException)
            {
                PhysicalMesh2D physMesh = GetMesh(name).ToPhysicalMesh();
                PhysMeshes.Add(name, physMesh);
                return physMesh;
            }
        }*/

        private readonly SortedDictionary<string, Charmap> Charmaps = new();
        public Charmap GetCharmap(string name)
        {
            try
            {
                return Charmaps[name];
            }
            catch (KeyNotFoundException)
            {
                Charmap charmap = new(name, Core);
                Charmaps.Add(name, charmap);
                return charmap;
            }

        }

        private readonly SortedDictionary<string, Sound> Sounds = new();
        public Sound GetSound(string name)
        {
            try
            {
                return Sounds[name];
            }
            catch (KeyNotFoundException)
            {
                Sound sound = new(name, Core.Audio);
                Sounds.Add(name, sound);
                return sound;
            }

        }
    }
}
