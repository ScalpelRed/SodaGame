using Silk.NET.OpenGL;
using System.Numerics;
using System.Text;
using Game.Assets;

namespace Game.Graphics
{
    public class GlShader
    {
        public static string AppendKeyword = "$Append ";
        public static string ShaderTypeKeyword = "$ShaderType ";

        public readonly uint Handle;
        private readonly Dictionary<string, ShaderUniform> Uniforms;
        public readonly OpenGL Gl;

        public readonly string Name;

        public GlShader(OpenGL gl)
        {
            Gl = gl;
            Handle = 0;
            Name = "Empty";
            Uniforms = new();
            //Uniforms = Array.Empty<ShaderUniform>();
        }

        public GlShader(string name, OpenGL opengl)
        {
            Gl = opengl;
            Name = name;

            Handle = opengl.Api.CreateProgram();

            List<uint> subshaderHandles = new();
            Dictionary<string, string> uniformLines = new();

            uint compile(string code, ShaderType shaderType)
            {
                uint handle = opengl.Api.CreateShader(shaderType);
                opengl.Api.ShaderSource(handle, code);
                opengl.Api.CompileShader(handle);
                subshaderHandles.Add(handle);
                string log = opengl.Api.GetShaderInfoLog(handle);
                opengl.Core.Log($"--- Shader log {name} : {shaderType} ---\n{log}");
                return handle;
            }

            void processFile(string name, List<string> prevFiles)
            {
                if (prevFiles.Contains(name)) throw new FormatException($"Shader recursion detected (file {name} was attached before)");
                else prevFiles.Add(name);

                string sourceCode;
                try
                {
                    sourceCode = opengl.Core.Platform.IO.ReadAllTextReadable($"shaders/{name}.shader");
                }
                catch (IOException)
                {
                    throw new AssetNotFoundException("shader", name);
                }

                string[] lines = Util.UtilFunc.SplitLines(sourceCode);

                StringBuilder code = new();
                ShaderType shaderType = 0;
                foreach (string line in lines)
                {
                    if (line.StartsWith(AppendKeyword))
                    {
                        if (code.Length > 0)
                        {
                            if (shaderType == 0) throw new FormatException($"Shader type is not specified (file {name})");
                            opengl.Api.AttachShader(Handle, compile(code.ToString(), shaderType));
                            code.Clear();
                        }

                        string[] words = line.Split(' ');
                        processFile(words[1], prevFiles);
                    }
                    else if (line.StartsWith(ShaderTypeKeyword))
                    {
                        if (code.Length > 0)
                        {
                            if (shaderType == 0) throw new FormatException($"Shader type is not specified (file {name})");
                            opengl.Api.AttachShader(Handle, compile(code.ToString(), shaderType));
                            code.Clear();
                        }

                        string[] words = line.Split(' ');
                        if (Enum.IsDefined(typeof(ShaderType), words[1])) shaderType = Enum.Parse<ShaderType>(words[1], true);
                        else throw new FormatException($"Invalid shader type \"{words[1]}\" (file {name})");
                    }
                    else if (line.StartsWith("uniform "))
                    {
                        string[] split = line.Split(' ');
                        uniformLines.TryAdd(split[2][..^1], split[1]);
                        code.Append(line + "\n");
                    }
                    else if (line.Length > 0)
                    {
                        code.Append(line + "\n");
                    }
                }
                if (code.Length > 0)
                {
                    if (shaderType == 0) throw new FormatException($"Shader type is not specified (file {name})");
                    opengl.Api.AttachShader(Handle, compile(code.ToString(), shaderType));
                }
            }

            processFile(name, new());
            opengl.Api.LinkProgram(Handle);

            foreach (uint h in subshaderHandles) opengl.Api.DeleteShader(h);

            Uniforms = new();
            foreach (var line in uniformLines)
            {
                if (Enum.TryParse<ShaderUniform.ShaderValueType>(line.Value, true, out var type))
                    Uniforms.Add(line.Key, new(this, type, line.Key));
                else Uniforms.Add(line.Key, new(this, 0, line.Key));
            }

            string log;
            try
            {
                // Sometimes it throws exception because some inner array is empty
                log = opengl.Api.GetProgramInfoLog(Handle);
            }
            catch (ArgumentException)
            {
                log = "";
            }

            opengl.Core.Log($"--- Shader log {name} : GENERAL ---\n{log}");
        }

        public bool IsEmpty() => Handle <= 0;


        public void SetUniform(string name, object value)
        {
            if (Uniforms.TryGetValue(name, out ShaderUniform? uniform))
            {
                uniform.SetValue(value);
            }
        }

        public void ApplyUniforms()
        {
            foreach (ShaderUniform v in Uniforms.Values) v.Apply();
        }

        public int GetDirectUniformLocation(string name)
        {
            return Gl.Api.GetUniformLocation(Handle, name);
        }

        public void DirectUniformInt(int pos, int value)
        {
            Gl.Api.Uniform1(pos, value);
        }

        public void DirectUniformFloat(int pos, float value)
        {
            Gl.Api.Uniform1(pos, value);
        }

        public void DirectUniformDouble(int pos, double value)
        {
            Gl.Api.Uniform1(pos, value);
        }

        public void DirectUniformVec2(int pos, Vector2 value)
        {
            Gl.Api.Uniform2(pos, ref value);
        }

        public void DirectUniformVec3(int pos, Vector3 value)
        {
            Gl.Api.Uniform3(pos, ref value);
        }

        public void DirectUniformVec4(int pos, Vector4 value)
        {
            Gl.Api.Uniform4(pos, ref value);
        }

        public unsafe void DirectUniformMat4(int pos, Matrix4x4 value)
        {
            Gl.Api.UniformMatrix4(pos, 1, true, (float*)&value);
        }

        public override bool Equals(object? obj) => ReferenceEquals(this, obj);

        public override int GetHashCode()
        {
            return (int)Handle;
        }
    }
}
