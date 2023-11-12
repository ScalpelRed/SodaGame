$ShaderType FragmentShader
#version 310 es

precision mediump sampler2D;
precision mediump float;

uniform sampler2D tex;
uniform vec4 tint;

smooth in vec2 texcoord;
smooth in float normalCoef;

out vec4 outColor;

const float lightCoef = 0.8;

float lightFunc(float n) {
	return n + lightCoef * (1 - n);
}

void main(){
	outColor = texture(tex, texcoord) * tint;
	outColor.rgb *= lightFunc(normalCoef);
}
