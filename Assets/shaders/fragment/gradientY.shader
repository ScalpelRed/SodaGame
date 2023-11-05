$ShaderType FragmentShader
#version 310 es

precision lowp sampler2D;
precision lowp float;

smooth in vec2 texcoord;

uniform sampler2D tex;
uniform vec3 upColor;
uniform vec3 downColor;

out vec4 outColor;

void main(){
	outColor.rgb = mix(upColor, downColor, texcoord.y);
	outColor.a = 1.0;
}