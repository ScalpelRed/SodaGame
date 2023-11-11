using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using System.Numerics;
using Game.Main;
using Game.OtherAssets;
using System.Diagnostics;

namespace Game.Graphics
{
    public class OpenGL
    {
        public IView View;
        public GL Api;
        public GameCore Core;

        public Vector2 ScreenSize;
        public float AspectRatio;

        private int frameCounter;
        private float deltaTime;

        protected DateTime FpsTime;
        protected int FpsFrame;
        public double AverageFps { get; protected set; }
        public int FrameCounter { get => frameCounter; set => frameCounter = value; }
        public float DeltaTime { get => deltaTime; set => deltaTime = value; }

        public readonly Stencil Stencil;

        public OpenGL(IView view, GameCore core)
        {
            Core = core;

            View = view;
            Api = GL.GetApi(view);

            string openglInfo = "OpenGL info:";
            openglInfo += $"\n   Version: {Api.GetStringS(StringName.Version)}";
            openglInfo += $"\n   GLSL version: {Api.GetStringS(StringName.ShadingLanguageVersion)}";
            openglInfo += $"\n   Renderer: {Api.GetStringS(StringName.Renderer)}";
            openglInfo += $"\n   Vendor: {Api.GetStringS(StringName.Vendor)}";
            Api.GetInteger(GetPName.MaxTextureSize, out int maxTexSize);
            openglInfo += $"\n   Max texture size: {maxTexSize}";
            Api.GetInteger(GetPName.Max3DTextureSize, out int max3DTexSize);
            openglInfo += $"\n   Max 3D texture size: {max3DTexSize}";
            Core.Log(openglInfo);

            ScreenSize = new Vector2(View.Size.X, View.Size.Y);
            
            AspectRatio = ScreenSize.Y / ScreenSize.X;

            Api.Enable(EnableCap.Blend);
            Api.BlendFunc(BlendingFactor.SrcAlpha,
                BlendingFactor.OneMinusSrcAlpha);

            /*Api.Enable(EnableCap.DepthTest);
            Api.DepthFunc(DepthFunction.Less);*/

            Api.Enable(EnableCap.Multisample);

            Api.Enable(EnableCap.StencilTest);
            Api.ClearStencil(0);

            Api.ClearColor(0.5f, 0.5f, 0, 1);
            Api.Clear(ClearBufferMask.ColorBufferBit);
            //Api.Clear(ClearBufferMask.DepthBufferBit);

            Api.Enable(EnableCap.CullFace);

            View.Render += (double t) => RenderFrame(t);
            View.Resize += (Silk.NET.Maths.Vector2D<int> size) =>
            {
                ScreenSize = new Vector2(size.X, size.Y);
                AspectRatio = ScreenSize.Y / ScreenSize.X;
                Api.Viewport(0, 0, (uint)size.X, (uint)size.Y);
                Resized?.Invoke();
            };

            Stencil = new Stencil(Api);
        }

        /*protected List<Renderer> RenderersToDraw = new();
        protected int DrawCount = 0;

        public void AllowDraw(Renderer rend)
        {
            if (RenderersToDraw.Count <= DrawCount) RenderersToDraw.Add(rend);
            else RenderersToDraw[DrawCount] = rend;
            DrawCount++;
        }*/

        public void RenderFrame(double delta)
        {
            deltaTime = (float)delta;

            Api.Clear(ClearBufferMask.ColorBufferBit);
            Api.StencilMask(0xFF);
            Api.Clear(ClearBufferMask.StencilBufferBit);
            Api.StencilMask(0x00);
            //Api.Clear(ClearBufferMask.DepthBufferBit);

            Core.Game.Step();

            View.SwapBuffers();

            FrameCounter++;

            FpsFrame++;
            if ((DateTime.Now - FpsTime).TotalSeconds >= 1)
            {
                AverageFps = FpsFrame / (DateTime.Now - FpsTime).TotalSeconds;
                FpsTime = DateTime.Now;
                FpsFrame = 0;
                Core.Log(AverageFps);
            }
        }

        public event Action? Resized;

        static void Main()
        {
            // unused
        }
    }
}                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                       