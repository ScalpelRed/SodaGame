using Game.Graphics;
using Game.Main;
using Game.Text.Ttf2mesh;
using Game.UI;
using Game.Util;
using System.Numerics;
using static Game.UI.UITransform;

namespace Game.Text
{
	public class UITextRenderer : UIRenderer
	{
		private ReferenceValue<string> text;
		public ReferenceValue<string> Text
		{
			get => text;
			set
			{
				text.Changed -= (string str) => NeedsUpdate = true;
				text = value;
				text.Changed += (string str) => NeedsUpdate = true;
				NeedsUpdate = true;
			}
		}

		private IGlyphProvider font;
		public IGlyphProvider Font
		{
			get => font;
			set
			{
				font = value;
				NeedsUpdate = true;
			}
		}

		private float scale = 50;
		public float Scale
		{
			get => scale;
			set
			{
				scale = value;
				NeedsUpdate = true;
			}
		}

		private float lineHeight = 1;
		public float LineHeight
		{
			get => lineHeight;
			set
			{
				lineHeight = value;
				NeedsUpdate = true;
			}
		}

		private float alignmentX = -1;
		public float AlignmentX
		{
			get => alignmentX;
			set
			{
				alignmentX = value;
				NeedsUpdate = true;
			}
		}

		private float alignmentY = 1;
		public float AlignmentY
		{
			get => alignmentY;
			set
			{
				alignmentY = value;
				NeedsUpdate = true;
			}
		}

		public GlModel Model { get; protected set; }

		private float VerticalAlignment = 0f;
		private readonly List<Vector2> LineAlignments = [];
		private readonly List<Char> CharData = [];

		public UITextRenderer(WorldObject linkedObject, ReferenceValue<string> text, IGlyphProvider font) : base(linkedObject)
		{
			this.text = text;
			this.font = font;
			Model = new GlModel(Game.Core.OpenGL, Game.Core.Assets.Shaders.Get("textSolid"));

			text.Changed += (string _) => NeedsUpdate = true;
			UITransform.Changed += () => NeedsUpdate = true;

            SetValue("color", Vector4.UnitW);

            NeedsUpdate = true;
		}

		public UITextRenderer(WorldObject linkedObject, string text, IGlyphProvider font) : this(linkedObject, new ReferenceValue<string>(text), font)
		{
			
		}

		private bool NeedsUpdate = true;

		private void UpdateIfNecessary()
		{
			if (NeedsUpdate)
			{
				CharData.Clear();
				LineAlignments.Clear();
                VerticalAlignment = lineHeight * scale * 0.5f;

                float cposX = 0;
				int line = 0;

				float edgeX = (UITransform.Bound2.X - UITransform.Bound1.X) * 0.5f * alignmentX;

                for (int i = 0; i < Text.Value.Length; i++)
				{
					char c = Text.Value[i];
					Glyph? glyph = Font.GetGlyphFor(c);
					GlMesh? mesh = Font.GetGlMeshFor(c);

					CharData.Add(new Char(c, glyph, mesh, cposX, line));

					if (glyph is not null)
					{
						cposX += glyph.Advance * scale;
					}

					if (c == '\n')
					{
						LineAlignments.Add(new Vector2(edgeX - cposX * (alignmentX + 1) * 0.5f, -lineHeight * scale * line));
						cposX = 0;
						line++;
					}
				}

                LineAlignments.Add(new Vector2(edgeX - cposX * (alignmentX + 1) * 0.5f, -lineHeight * scale * line));
                VerticalAlignment += (UITransform.Bound2.Y - UITransform.Bound1.Y - lineHeight * scale * (line + 1)) * 0.5f * alignmentY;

                NeedsUpdate = false;
			}
		}

		public override void Draw(Camera camera)
		{
			UpdateIfNecessary();

			Model.Shader.SetUniform("transform", UITransform.InheritableMatrix);
			Model.Shader.SetUniform("camera", camera.ViewMatrix);
			foreach (var value in Values) Model.Shader.SetUniform(value.Key, value.Value);

			Matrix4x4 texTransform = Matrix4x4.CreateScale(scale);
			foreach (Char c in CharData)
			{
				if (c.Mesh is not null)
				{
					Model.Mesh = c.Mesh;

					Vector2 lineAl = LineAlignments[c.Line];
					texTransform.M41 = c.PosX + lineAl.X;
					texTransform.M42 = lineAl.Y + VerticalAlignment;

					Model.Shader.SetUniform("textransform", texTransform);
					Model.Draw();
				}
			}
		}

		private record Char(char Character, Glyph? Glyph, GlMesh? Mesh, float PosX, int Line);
	}
}
