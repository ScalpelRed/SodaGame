using System.Numerics;
using Silk.NET.Input;

using Game.Graphics;
using Triode;

namespace Game.Main
{
	public sealed class Input
	{
		
		private readonly IInputContext InputContext;

		public event Action<Key>? KeyDown;
		public event Action<Key>? KeyUp;
		public event Action<char>? CharDown;

		public event Action<Vector2>? MouseMove;
		public event Action<MouseButton>? MouseDown;
		public event Action<MouseButton>? MouseUp;

		public Vector2 MousePosition { get; private set; }

		private readonly OpenGL Gl;

		public Input(OpenGL gl)
		{
			Gl = gl;
			InputContext = gl.View.CreateInput();
			RefreshDevices();
		}

		private void RefreshDevices()
        {
			foreach (IKeyboard v in InputContext.Keyboards)
			{
				v.KeyDown += (IKeyboard _, Key key, int keyid) => KeyDown?.Invoke(key);
				v.KeyUp += (IKeyboard _, Key key, int keyid) => KeyUp?.Invoke(key);
				v.KeyChar += (IKeyboard _, char c) => CharDown?.Invoke(c);
			}

			foreach (IMouse v in InputContext.Mice)
			{
				v.MouseMove += (IMouse mouse, Vector2 pos) =>
				{
					pos -= Gl.ScreenSize * 0.5f;
					MousePosition = new Vector2(pos.X, -pos.Y);
					MouseMove?.Invoke(MousePosition);
					Gl.Core.Game.MainCamera.WorldToScreen(pos);
				};
				v.MouseDown += (IMouse mouse, MouseButton button) => MouseDown?.Invoke(button);
				v.MouseUp += (IMouse mouse, MouseButton button) => MouseUp?.Invoke(button);
			}
		}
	}
}
