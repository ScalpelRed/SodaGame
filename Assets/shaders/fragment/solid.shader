$ShaderType FragmentShader
#version 310 es

precision lowp sampler2D;
precision lowp float;

uniform vec4 inColor;

out vec4 outColor;

void main(){
	outColor = inColor;
}