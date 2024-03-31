using System.Numerics;
using System.Runtime.InteropServices;
using Triode.Game.ExactGame;
using Triode.Game.ExactGame.Items;
using Triode.Game.ExactGame.SodaLayers;
using Triode.Game.ExactGame.SodaScenes;
using Triode.Game.ExactGame.UI;
using Triode.Game.General;
using Triode.Game.Graphics;
using Triode.Game.Text;
using Triode.Game.UI;
using Triode.Game.Util;

namespace Triode.Game.General
{
    public class GameController
    {
        public readonly GameCore Core;
        public Camera MainCamera;

        public float DeltaTime { get => Core.OpenGL.DeltaTime; }

        public readonly Multifont Fonts;

        /*public readonly List<LayerParameters> Layers = [];
        public readonly List<SodaScene> Sodas = [];
        SodaScene? ActiveSoda;*/

        public readonly Tabs UITabs;

        public readonly Dictionary<string, Item> Inventory = [];

        public GameController(GameCore core)
        {
            Core = core;

            MainCamera = new Camera(core.OpenGL.ScreenSize.X, core.OpenGL.ScreenSize.Y, 200, 0.01f, new WorldObject(Vector3.Zero, core));
            core.OpenGL.Resized += () =>
            {
                MainCamera.Width = core.OpenGL.ScreenSize.X;
                MainCamera.Height = core.OpenGL.ScreenSize.Y;
            };

            Fonts = new Multifont(core, "Arial");
            //Fonts = core.Assets.Multifonts.Get("default");

            //AddItem("bubble", new ItemBubble(core), 0);

            /*LayerParameters layer1 = new(1, 100, 50f, 50f);
            Layers.Add(layer1);
            Layers.Add(LayerParameters.Scale(layer1, 0.75f, 2));
            Layers.Add(LayerParameters.Scale(layer1, 0.5f, 3));

            Sodas.Add(
                new DefaultSodaScene(new(Vector3.Zero, this), new SodaInfo("Soda/Orange/Name",   "RESERVED TEXT FIELD ========"), new Vector3(1, 0.5f, 0)),
                new DefaultSodaScene(new(Vector3.Zero, this), new SodaInfo("Soda/Tarkhuna/Name", "RESERVED TEXT FIELD ========"), new Vector3(0, 0.75f, 0)));*/

            UITabs = new Tabs(new WorldObject(new UITransform(core), core));
            UITabs.SetColor(new Vector3(0.5f, 0.5f, 0.5f));

            //Core.Assets.GlMeshes.Enlist("jellyCubeRigged", Jelly.CreateRiggedGlMesh(Core.OpenGL, Core.Assets.RawMeshes.Get("jellyCube"), -Vector3.UnitY));
            //Core.Assets.GlMeshes.Enlist("jellyTest", Jelly.CreateRiggedGlMesh(Core.OpenGL, Core.Assets.RawMeshes.Get("jellyTest"), -Vector3.UnitY));

            //SetActiveSoda(Sodas[0]);

            //core.Assets.GetSound("pop");
        }

        /*public void SetActiveSoda(SodaScene soda)
        {
            if (soda != ActiveSoda)
            {
                ActiveSoda?.SetInactive();

                ActiveSoda = soda;
                ActiveSoda.SetActive();
                UITabs.SetColor(ActiveSoda.UIColor);
            }
        }*/

        public bool TryGetItem(string name, out Item? item)
        {
            return Inventory.TryGetValue(name, out item);
        }

        public Item GetItem(string name)
        {
            return Inventory[name];
        }

        public void AddItem(string name, Item item, float initialCount = 0)
        {
            Inventory.Add(name, item);
            item.Count = initialCount;
        }

        public void Step()
        {
            //ActiveSoda?.Step();
            UITabs.Step();
        }
    }
}
