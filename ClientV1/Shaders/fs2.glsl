#version 330 core

in vec2 outUV;
out vec3 color;
uniform sampler2D myTextureSampler;

void main(){
    color = texture( myTextureSampler, outUV ).rgb;
}