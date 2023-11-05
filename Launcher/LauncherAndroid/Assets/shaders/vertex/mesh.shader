$ShaderType VertexShaderArb
#version 310 es

layout (location = 0) in vec3 Position;
layout (location = 1) in vec2 Texcoord;
layout (location = 2) in vec3 Normal;

uniform mat4 transform;
uniform mat4 camera;

smooth out vec2 texcoord;

void main(){
	gl_Position = vec4(Position, 1) * transform * camera;

	texcoord = Texcoord;
}