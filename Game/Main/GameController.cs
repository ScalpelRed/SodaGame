using System.Numerics;
using Game.ExactGame;
using Game.ExactGame.Items;
using Game.ExactGame.SodaScenes;
using Game.ExactGame.UI;
using Game.Graphics;
using Game.Text;

namespace Game.Main
{
    public class GameController
    {
        public readonly GameCore Core;
        public Camera MainCamera;

        public float DeltaTime { get => Core.OpenGL.DeltaTime; }

        public readonly Multifont Fonts;

        public readonly List<BubbleLayer> Layers = [];
        public readonly List<SodaScene> Sodas = [];
        SodaScene? ActiveSoda;

        public readonly Tabs UITabs;

        public readonly List<Item> Inventory = [];

        public GameController(GameCore core)
        {
            Core = core;

            MainCamera = new Camera(new WorldObject(Vector3.Zero, this), core.OpenGL, 200);

            //Fonts = new Multifont(core, "Arial");
            //Fonts = core.Assets.Multifonts.Get("default");

            AddItemSlot(new ItemBubble(core), 0);
            //GetItemSlot<ItemBubble>()!.CountChanged += (float v) => Console.WriteLine(v);

            BubbleLayer layer1 = new(1, 100, 50f, 50f);
            Layers.Add(layer1);
            Layers.Add(BubbleLayer.Scale(layer1, 0.75f, 2));
            Layers.Add(BubbleLayer.Scale(layer1, 0.5f, 3));

            Sodas.Add(new DefaultSodaScene(new(Vector3.Zero, this), new Vector3(1, 0.5f, 0)));
            Sodas.Add(new DefaultSodaScene(new(Vector3.Zero, this), new Vector3(0, 0.75f, 0)));

            UITabs = new Tabs(new WorldObject(Vector3.Zero, this));

            //Core.Assets.GlMeshes.Enlist("jellyCube", Jelly.CreateRiggedGlMesh(Core.OpenGL, Core.Assets.RawMeshes.Get("jellyCube"), -Vector3.UnitY));
            //Core.Assets.GlMeshes.Enlist("jellyTest", Jelly.CreateRiggedGlMesh(Core.OpenGL, Core.Assets.RawMeshes.Get("jellyTest"), -Vector3.UnitY));

            SetActiveSoda(Sodas[0]);

            //core.Assets.GetSound("pop");
        }

        public void SetActiveSoda(SodaScene soda)
        {
            if (soda != ActiveSoda)
            {
                ActiveSoda?.SetInactive();

                ActiveSoda = soda;
                ActiveSoda.SetActive();
                UITabs.SetColor(ActiveSoda.UIColor);
            }
        }

        public bool TryGetItemSlot<T>(out T? slot)
        {
            foreach (Item v in Inventory)
            {
                if (v is T res)
                {
                    slot = res;
                    return true;
                }
            }
            slot = default;
            return false;
        }

        public T? GetItemSlot<T>()
        {
            foreach (Item v in Inventory) if (v is T res) return res;
            return default;
        }

        public void AddItemSlot(Item slot, float initialCount = 0)
        {
            Inventory.Add(slot);
            slot.Count += initialCount;
        }

        public void Step()
        {
            ActiveSoda?.Step();
            UITabs.Step();
        }
    }
}
