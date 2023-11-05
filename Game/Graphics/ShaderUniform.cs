using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Game.Graphics
{
    public class ShaderUniform
    {
        public readonly GlShader Shader;
        private readonly int Location;

        private object Value;

        private bool Changed;

        private readonly Action<GlShader, int, object> apply;

        public ShaderUniform(GlShader shader, ShaderValueType type, string name)
        {
            Shader = shader;
            Location = Shader.GetDirectUniformLocation(name);
            Changed = true;
            switch (type)
            {
                case ShaderValueType.Int:
                    Value = 0;
                    apply = (GlShader shad, int loc, object val) => shad.DirectUniformInt(loc, (int)val);
                    break;
                case ShaderValueType.Float:
                    Value = 0f;
                    apply = (GlShader shad, int loc, object val) => shad.DirectUniformFloat(loc, (float)val);
                    break;
                case ShaderValueType.Double:
                    Value = 0.0;
                    apply = (GlShader shad, int loc, object val) => shad.DirectUniformDouble(loc, (double)val);
                    break;
                case ShaderValueType.Vec2:
                    Value = Vector2.Zero;
                    apply = (GlShader shad, int loc, object val) => shad.DirectUniformVec2(loc, (Vector2)val);
                    break;
                case ShaderValueType.Vec3:
                    Value = Vector3.Zero;
                    apply = (GlShader shad, int loc, object val) => shad.DirectUniformVec3(loc, (Vector3)val);
                    break;
                case ShaderValueType.Vec4:
                    Value = Vector4.Zero;
                    apply = (GlShader shad, int loc, object val) => shad.DirectUniformVec4(loc, (Vector4)val);
                    break;
                case ShaderValueType.Mat4:
                    Value = Matrix4x4.Identity;
                    apply = (GlShader shad, int loc, object val) => shad.DirectUniformMat4(loc, (Matrix4x4)val);
                    break;
                default:
                    Value = 0;
                    apply = (GlShader shad, int loc, object val) => { };
                    break;
            }

        }

        public void SetValue(object value)
        {
            if (Value.Equals(value)) return;

            Value = value;
            Changed = true;
        }

        public void Apply()
        {
            if (Changed)
            {
                Changed = false;
                apply.Invoke(Shader, Location, Value);
            }
        }

        public enum ShaderValueType
        {
            Int,
            Float,
            Double,
            Vec2,
            Vec3,
            Vec4,
            Mat4,
        }
    }
}
