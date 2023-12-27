$ShaderType VertexShaderArb
#version 310 es

layout (location = 0) in vec3 Position;
layout (location = 1) in vec2 Texcoord;
layout (location = 2) in vec3 Normal;
layout (location = 3) in float JellyWeight;

uniform mat4 transform;
uniform mat4 camera;
uniform vec3 bone; // idk how to name properly

smooth out vec2 texcoord;
smooth out float normalCoef;

void main(){
	gl_Position = vec4(Position + bone * JellyWeight, 1) * transform * camera;

	texcoord = Texcoord;

	vec4 n = vec4(Normal, 0);
	normalCoef = dot(vec4(0, 1, 0, 0), normalize(n * transform));
}