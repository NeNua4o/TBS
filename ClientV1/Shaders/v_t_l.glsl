﻿#version 330

layout(location = 0) in vec3 vertexPosition_modelspace;
layout(location = 1) in vec2 vertexUV;
layout(location = 2) in vec3 vertexNormals;

out vec2 outUV;
out vec3 Normal;
out vec3 FragPos;

uniform mat4 MVP;
uniform mat4 M;
uniform mat4 V;
uniform mat4 MR;

void main(){
  
  gl_Position =  MVP * vec4(vertexPosition_modelspace, 1);
  FragPos = vec3(M * vec4(vertexPosition_modelspace, 1));
  outUV = vertexUV;
  Normal = vec3(MR * vec4(vertexNormals, 1));
}