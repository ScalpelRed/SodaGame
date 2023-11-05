using System.Numerics;

namespace Game.Util
{
    public struct Vertex
    {
        public Vector3 Position;
        public Vector2 Texcoords;
        public Vector3 Normal;

        public Vertex(Vector3 pos, Vector2 texcoords, Vector3 normal)
        {
            Position = pos;
            Texcoords = texcoords;
            Normal = normal;
        }

        public override string ToString()
        {
            return $"{Position} {Texcoords} {Normal}";
        }
    }
}
