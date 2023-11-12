$ShaderType FragmentShader
#version 310 es

precision lowp sampler2D;
precision lowp float;

uniform vec4 color;

out vec4 outColor;

void main(){
	outColor = color;
}