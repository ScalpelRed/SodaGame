using System.Numerics;


namespace Game.Util
{
    public struct Rectangle
    {
        public static readonly Rectangle Zero = new Rectangle(Vector2.Zero, Vector2.Zero);
        public static readonly Rectangle Half = ByCenterAndSize(Vector2.Zero, Vector2.One * 0.5f);
        public static readonly Rectangle One = ByCenterAndSize(Vector2.Zero, Vector2.One * 0.5f);

        private Vector2 point1 = Vector2.Zero;
        private Vector2 point2 = Vector2.Zero;

        public Vector2 Point1
        {
            readonly get => point1;
            set
            {
                point1 = value;
                UpdateOthers();
            }
        }

        public Vector2 Point2
        {
            readonly get => point2;
            set
            {
                point2 = value;
                UpdateOthers();
            }
        }

        public Vector2 Max { get; private set; }
        public Vector2 Min { get; private set; }
        public Vector2 Center { get; private set; }

        private void UpdateOthers()
        {
            Center = (point1 + point2) * 0.5f;

            Vector2 max = Vector2.Zero;
            Vector2 min = Vector2.Zero;

            if (point1.X > point2.X) // using MathF.Max/Min will cost one more 'if'
            {
                max.X = point1.X;
                min.X = point2.X;
            }
            else
            {
                max.X = point2.X;
                min.X = point1.X;
            }

            if (point1.Y > point2.Y)
            {
                max.Y = point1.Y;
                min.Y = point2.Y;
            }
            else
            {
                max.Y = point2.Y;
                min.Y = point1.Y;
            }

            Max = max;
            Min = min;
        }

        public static Rectangle ByCenterAndSize(Vector2 pos, Vector2 size) => new(pos + size, pos - size);

        public Rectangle(Vector2 point1, Vector2 point2)
        {
            Point1 = point1;
            Point2 = point2;
        }

        public readonly Rectangle Transform(Matrix4x4 matrix)
        {
            return new Rectangle(Vector2.Transform(Point1, matrix), Vector2.Transform(Point2, matrix));
        }

        public readonly bool Contains(Vector2 pos)
        {
            return Min.X < pos.X && pos.X < Max.X && Min.Y < pos.Y && pos.Y < Max.Y;
        }

        public override readonly string ToString()
        {
            return $"{Point1}/{Point2}";
        }
    }
}
