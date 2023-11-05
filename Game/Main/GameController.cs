using System.Diagnostics;
using System.Numerics;
using Game.ExactGame;
using Game.ExactGame.SodaScenes;
using Game.ExactGame.SodaScreens;
using Game.ExactGame.UI;
using Game.Graphics;
using Game.Graphics.Renderers;
using Game.Main;
using Game.Phys;
using Silk.NET.Input;

namespace Game.Main
{
    public class GameController
    {
        public readonly GameCore Core;
        public Camera MainCamera;

        public float DeltaTime { get => Core.OpenGL.DeltaTime; }

        public readonly List<BubbleLayer> Layers = new();
        public readonly List<SodaScene> Sodas = new();
        SodaScene ActiveSoda;

        readonly BottomPanel BottomPanel;

        public GameController(GameCore core)
        {
            Core = core;
            MainCamera = new Camera(new WorldObject(Vector3.Zero, this), core.OpenGL, 10);

            BottomPanel = new BottomPanel(new WorldObject(Vector3.Zero, this));

            BubbleLayer layer1 = new(1, 100, 50f, 50f);
            Layers.Add(layer1);
            Layers.Add(BubbleLayer.Scale(layer1, 0.75f, 2));
            Layers.Add(BubbleLayer.Scale(layer1, 0.5f, 3));

            Sodas.Add(new DefaultSodaScene(new(Vector3.Zero, this), new Vector3(1, 0.5f, 0)));
            ActiveSoda = Sodas[0];
            SetActiveSoda(0);

            //core.Assets.GetSound("pop");
        }

        public void SetActiveSoda(int index)
        {
            ActiveSoda = Sodas[index];
            BottomPanel.SetColor(ActiveSoda.GetUIColor());
        }

        public void Step()
        {
            ActiveSoda.Step();
            BottomPanel.Step();

        }
    }

}
