$ShaderType FragmentShader
#version 310 es

precision lowp sampler2D;
precision lowp float;

out vec4 outColor;

void main(){
	if (int(0.5 * gl_FragCoord.x) % 2 == 0 && int(0.5 * gl_FragCoord.y) % 2 == 0) outColor = vec4(1, 0, 0, 1);
}