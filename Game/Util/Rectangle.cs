using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.MathF;
using Game.Util;


namespace Game.Util
{
    public struct Rectangle
    {
        public static readonly Rectangle One = ByCenterAndSize(Vector2.Zero, Vector2.One * 0.5f);

        private Vector2 P1 = Vector2.Zero;
        private Vector2 P2 = Vector2.Zero;

        public float Width { get => MathF.Abs(P1.X - P2.X); }
        public float Height { get => MathF.Abs(P1.Y - P2.Y); }

        public static Rectangle ByCenterAndSize(Vector2 pos, Vector2 size) => new(pos + size, pos - size);

        public Rectangle(Vector2 p1, Vector2 p2)
        {
            P1 = p1;
            P2 = p2;
        }

        public Rectangle Transform(Matrix4x4 matrix)
        {
            return new Rectangle(Vector2.Transform(P1, matrix), Vector2.Transform(P2, matrix));
        }

        public bool Contains(Vector2 pos)
        {
            return UtilFunc.Between(P1.X, pos.X, P2.X) && UtilFunc.Between(P1.Y, pos.Y, P2.Y);
        }

        public override string ToString()
        {
            return $"{P1}/{P2}";
        }
    }
}
