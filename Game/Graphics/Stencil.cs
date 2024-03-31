using Silk.NET.OpenGL;

namespace Triode.Game.Graphics
{
    public class Stencil
    {
        public readonly GL Api;

        public Stencil(GL api)
        {
            Api = api;
        }

        public void DrawStencil()
        {
            Api.StencilMask(0xFF);
            Api.StencilFunc(StencilFunction.Always, 1, 0xFF);
            Api.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Replace);
        }

        public void DrawAll()
        {
            Api.StencilMask(0x00);
            Api.StencilFunc(StencilFunction.Always, 1, 0xFF);
            Api.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Replace);
        }

        public void DrawHidden()
        {
            Api.StencilMask(0x00);
            Api.StencilFunc(StencilFunction.Notequal, 1, 0xFF);
            Api.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Replace);
        }

        public void DrawLimited()
        {
            Api.StencilMask(0x00);
            Api.StencilFunc(StencilFunction.Equal, 1, 0xFF);
            Api.StencilOp(StencilOp.Keep, StencilOp.Keep, StencilOp.Replace);
        }
    }
}
