$ShaderType FragmentShader
#version 310 es

precision mediump sampler2D;
precision mediump float;

uniform sampler2D tex;
uniform vec4 tint;

smooth in vec2 texcoord;

out vec4 outColor;

void main(){
	outColor = texture(tex, texcoord) * tint;
}
