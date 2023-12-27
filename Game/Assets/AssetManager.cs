using Game.Audio;
using Game.Graphics;
using Game.Main;
using Game.OtherAssets;
using Game.Phys;
using Game.Text;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Assets
{
    public class AssetManager
    {
        public readonly GameCore Core;

        public AssetManager(GameCore core)
        {
            Core = core;
            
            Shaders = new Asset<string, GlShader>(core, (string name) => new(name, Core.OpenGL));
            RawTextures = new Asset<string, RawTexture>(core, (string name) => new(name, Core));
            Textures = new Asset<string, GlTexture>(core, (string name) => new(name, Core.OpenGL));
            SolidTextures = new Asset<Vector4, GlTexture>(core, (Vector4 color) => new(color, Core.OpenGL));
            RawMeshes = new Asset<string, RawMesh>(core, (string name) =>
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
                return new(source);
            });
            GlMeshes = new Asset<string, GlMesh>(core, (string name) => new(Core.OpenGL, RawMeshes.Get(name)));
            Sounds = new Asset<string, Sound>(core, (string name) => new(name, Core.Audio));
            Fonts = new Asset<string, Font>(core, (string name) => new(name, core));
            Multifonts = new Asset<string, Multifont>(core, (string name) => Multifont.FromFile(core, name));

            Shaders.Enlist("", new GlShader(core.OpenGL));
            RawTextures.Enlist("", new RawTexture(0, 0));
            Textures.Enlist("", new GlTexture());
            RawMeshes.Enlist("", new RawMesh());
            GlMeshes.Enlist("", new GlMesh());
        }

        public readonly Asset<string, GlShader> Shaders;
        public readonly Asset<string, RawTexture> RawTextures;
        public readonly Asset<string, GlTexture> Textures;
        public readonly Asset<Vector4, GlTexture> SolidTextures;
        public readonly Asset<string, RawMesh> RawMeshes;
        public readonly Asset<string, GlMesh> GlMeshes;
        public readonly Asset<string, Sound> Sounds;
        public readonly Asset<string, Font> Fonts;
        public readonly Asset<string, Multifont> Multifonts;
    }
}
