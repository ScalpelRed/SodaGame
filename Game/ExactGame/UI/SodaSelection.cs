using Game.ExactGame.SodaScenes;
using Game.Main;
using Game.UI;
using System.Numerics;

namespace Game.ExactGame.UI
{
    public sealed class SodaSelection : UIModule
    {
        private readonly float Opacity = 0.5f;
        private readonly UIModelRenderer Background;

        private readonly List<SodaListEntry> SodaEntries = [];
        private readonly UIList SodaList; 

        private const float EntrySpacing = 10f;

        public SodaSelection(WorldObject linkedObject) : base(linkedObject)
        {
            Transform.SetAnchoringX(UITransform.AnchoringX.Stretch);
            Transform.SetAnchoringY(UITransform.AnchoringY.Stretch);
            Transform.MarginDown = -BottomPanel.Height;

            Background = new UIModelRenderer(linkedObject, Game.Core.Assets.Shaders.Get("meshSolid"));

            SodaList = new UIList(linkedObject)
            {
                Offset = EntrySpacing
            };

            foreach (SodaScene s in Game.Sodas)
            {
                SodaListEntry e = new(UITransform.CreateObjectForUI(Game), s);
                SodaEntries.Add(e);
                SodaList.Affected.Add(e.Transform);
            }
        }

        public void SetColor(Vector3 color)
        {
            Background.SetValue("color", new Vector4(color, Opacity));
            foreach (SodaListEntry v in SodaEntries) v.SetColor(color);
        }

        public override void Step()
        {
            Game.Core.OpenGL.Stencil.DrawStencil();
            Background.Step();
            Game.Core.OpenGL.Stencil.DrawLimited();
            foreach (SodaListEntry v in SodaEntries) v.Step();
            Game.Core.OpenGL.Stencil.DrawAll();
        }
    }
}
