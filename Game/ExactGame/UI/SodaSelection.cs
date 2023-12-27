using Game.Graphics;
using Game.Main;
using System.Numerics;

namespace Game.ExactGame.UI
{
    public sealed class SodaSelection : ObjectModule
    {
        private readonly float Opacity = 0.5f;
        private readonly ModelRenderer Background;

        public List<SodaListEntry> SodaEntries = [];

        private const float EntrySpacing = 10f;

        public SodaSelection(WorldObject linkedObject) : base(linkedObject, false)
        {

            /*Background = new ModelRenderer(linkedObject, Game.Core.Assets.Shaders.Get("meshSolid"));

            //Transform.UISize2 = Game.Core.OpenGL.ScreenSize - Vector2.UnitY * BottomPanel.Height;

            float y = 0.5f * Game.Core.OpenGL.ScreenSize.Y - BottomPanel.Height;
            foreach (SodaScene s in Game.Sodas)
            {
                Console.WriteLine(y);
                SodaListEntry e = new(new WorldObject(new Vector3(0, y, 0), Game, Transform), s);
                e.Transform.Pivot = Vector3.UnitY;
                SodaEntries.Add(e);
                y -= e.Transform.UISize.Y + EntrySpacing;
            }*/
        }

        public void SetColor(Vector3 color)
        {
            Background.SetValue("color", new Vector4(color, Opacity));
            foreach (SodaListEntry v in SodaEntries) v.SetColor(color);
        }

        public override void Step()
        {
            /*Game.Core.OpenGL.Stencil.DrawStencil();
            Background.Step();
            Game.Core.OpenGL.Stencil.DrawLimited();
            foreach (SodaListEntry v in SodaEntries) v.Step();
            Game.Core.OpenGL.Stencil.DrawAll();*/
        }
    }
}
